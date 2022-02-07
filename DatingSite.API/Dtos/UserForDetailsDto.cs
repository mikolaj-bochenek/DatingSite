using System;
using System.Collections.Generic;
using PortalRandkowy.API.Models;

namespace PortalRandkowy.API.Dtos
{
    public class UserForDetailsDto
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

        //Additional Information

        public string Growth  { get; set; }
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public string MartialStatus { get; set; }
        public string Education { get; set; }
        public string Profession { get; set; }
        public string Children { get; set; }
        public string Languages { get; set; }

        //About Me
        public string Motto { get; set; }
        public string Description { get; set; }
        public string Personality { get; set; }
        public string LookingFor { get; set; }

        //Passions, Interests
        public string Interests { get; set; }
        public string FreeTime { get; set; }
        public string Activity { get; set; }
        public string Movies { get; set; }
        public string Music { get; set; }

        //Preferences
        public string ILike { get; set; }
        public string IDoNotLike { get; set; }
        public string MakesMeHappy { get; set; }
        public string FriendsWouldDescribeMe { get; set; }

        //Photos
        public ICollection<PhotosForDetiledDto> Photos { get; set; }
        public string PhotoUrl { get; set; }
    }
}