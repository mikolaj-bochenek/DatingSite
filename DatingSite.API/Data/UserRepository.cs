using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PortalRandkowy.API.Helpers;
using PortalRandkowy.API.Models;
using PortalRandkowy.API.Models.Data;

namespace PortalRandkowy.API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = _context.Users.Include(p => p.Photos).OrderByDescending(u => u.LastActive).AsQueryable();
            
            users = users.Where(u => u.Id != userParams.UserId);

            if (userParams.Gender != "both")
            {
                users = users.Where(u => u.Gender == userParams.Gender);
            }

            if (userParams.UserLikes)
            {
                var userLikes = await GetUserLikes(userParams.UserId, userParams.UserLikes);
                users = users.Where(u => userLikes.Contains(u.Id));
            }

            if (userParams.UserIsLikes)
            {
                var userIsLikes = await GetUserLikes(userParams.UserId, userParams.UserLikes);
                users = users.Where(u => userIsLikes.Contains(u.Id));
            }

            if (userParams.MinAge != 18 || userParams.MaxAge != 100)
            {
                var minDate = DateTime.Today.AddYears(-userParams.MaxAge -1);
                var maxDate = DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(u => u.DateOfBirth >= minDate && u.DateOfBirth <= maxDate);
            }

            if (userParams.City != "all")
            {
                users = users.Where(u => u.City == userParams.City);
            }

            if(!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "dateOfCreated": 
                        users = users.OrderByDescending(u => u.DateOfCreated);
                        break;

                    case "lastActive": 
                        users = users.OrderByDescending(u => u.LastActive);
                        break;

                    default:
                        users = users.OrderByDescending(u => u.DateOfBirth);
                        break;
                }
            }
            
            return await PagedList<User>.CreateListAsync(users, userParams.PageNumber, userParams.PageSize);
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SafeAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
            return photo;
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
           return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<Like> GetLike(int userId, int recipientId)
        {
            return await _context.Likes.FirstOrDefaultAsync(u => u.UserLikesId == userId && u.UserIsLikesId == recipientId);
        }

        private async Task<IEnumerable<int>> GetUserLikes(int id, bool userLikes)
        {
            var user = await _context.Users
                                .Include(x => x.UserLikes)
                                .Include(x => x.UserIsLikes)
                                .FirstOrDefaultAsync(u => u.Id == id);
            
            if (userLikes)
            {
                return user.UserLikes.Where(u => u.UserIsLikesId == id).Select(i => i.UserLikesId);
            }
            else
            {
                return user.UserIsLikes.Where(u => u.UserLikesId == id).Select(i => i.UserIsLikesId);
            }
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = _context.Messages.Include(u => u.Sender).ThenInclude(p => p.Photos)
                                            .Include(u => u.Recipient).ThenInclude(p => p.Photos).AsQueryable();
            
            switch (messageParams.MessageContainer)
            {
                case "Inbox" :
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId && u.RecipientDeleted == false);
                    break;
                case "Outbox" :
                    messages = messages.Where(u => u.SenderId == messageParams.UserId && u.SenderDeleted == false);
                    break;
                default :
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId && u.IsRead == false && u.RecipientDeleted == false);
                    break;
            }

            messages = messages.OrderByDescending(d => d.DateSent);

            return await PagedList<Message>.CreateListAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId)
        {
            var messages = await _context.Messages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                .Where(m => m.RecipientId == userId && m.SenderId == recipientId && m.RecipientDeleted == false
                       || m.RecipientId == recipientId && m.SenderId == userId && m.SenderDeleted == false)
                .OrderByDescending(m => m.DateSent)
                .ToListAsync();

            return messages;
            
        }  
    }
}