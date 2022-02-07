using System;
using PortalRandkowy.API.Models;

namespace PortalRandkowy.API.Dtos
{
    public class PhotosForDetiledDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateOfAdded { get; set; }
        public bool IsMain { get; set; }
    }
}