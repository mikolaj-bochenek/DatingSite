using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalRandkowy.API.Data;
using PortalRandkowy.API.Dtos;
using PortalRandkowy.API.Helpers;
using PortalRandkowy.API.Models;

namespace PortalRandkowy.API.Controllers
{
    //GET http://localhost:5000/api/users
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var userFromRepo = await _repo.GetUser(currentUserId);

                userParams.UserId = currentUserId;

                // if (userParams.Gender != "both")
                // {
                //     userParams.Gender = userFromRepo.Gender == "male" ? "male" : "female";
                // }

                var users = await _repo.GetUsers(userParams);

                var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

                Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

                return Ok(usersToReturn);
        }
            

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailsDto>(user);

            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var userFromRepo = await _repo.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if(await _repo.SafeAll())
                return NoContent();

            throw new Exception($"user update with id: {id} failed while saving to database");    
        }

        [HttpPost("{id}/like/{recipientId}")]
        public async Task<IActionResult> LikeUser(int id, int recipientId)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var like = await _repo.GetLike(id, recipientId);

            if (like != null)
                return BadRequest("You already like this user!");
            
            if (await _repo.GetUser(recipientId) == null)
                return NotFound();
            
            like = new Like
            {
                UserLikesId = id,
                UserIsLikesId = recipientId
            };

            _repo.Add<Like>(like);

            if (await _repo.SafeAll())
                return Ok();
                
            return BadRequest("User cannot be liked");
        }
    }
}