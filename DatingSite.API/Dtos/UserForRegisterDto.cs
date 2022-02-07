using System;
using System.ComponentModel.DataAnnotations;

namespace PortalRandkowy.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required(ErrorMessage = "username cannot be empty")]
        public string Username { get; set; }
        [Required(ErrorMessage = "password cannot be empty")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "password has to contain from 6 to 12 characters")]
        public string Password { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public DateTime DateOfCreated { get; set; }
        public DateTime LastActive { get; set; }

        public UserForRegisterDto()
        {
            DateOfCreated = DateTime.Now;
            LastActive = DateTime.Now;
        }
        
    }
}