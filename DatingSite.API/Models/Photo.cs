using System;

namespace PortalRandkowy.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateOfAdded { get; set; }
        public bool IsMain { get; set; }
        public string public_id { get; set; } //cloudinary additional property
        public User User { get; set; }
        public int UserId { get; set; }
    }
}