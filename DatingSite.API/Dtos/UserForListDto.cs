using System;

namespace PortalRandkowy.API.Dtos
{
    public class UserForListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }

        //General Information
        public string Gender { get; set; }
        public int Age { get; set; }
        public string ZodiacSign { get; set; }
        public DateTime DateOfCreated { get; set; }
        public DateTime LastActive { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
    }
}