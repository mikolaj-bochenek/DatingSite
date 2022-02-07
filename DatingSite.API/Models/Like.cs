namespace PortalRandkowy.API.Models
{
    public class Like
    {
        public int UserLikesId { get; set; }
        public int UserIsLikesId { get; set; }

        public User UserLikes { get; set; }
        public User UserIsLikes { get; set; }
    }
}