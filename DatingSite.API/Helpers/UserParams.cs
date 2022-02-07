namespace PortalRandkowy.API.Helpers
{
    public class UserParams
    {
        public const int MaxPageSize = 48;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 24;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        
        public int UserId { get; set; }
        public string Gender { get; set; } = "both";
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 100;
        public string City { get; set; } = "all";
        public string OrderBy { get; set; }
        public bool UserLikes { get; set; } = false;
        public bool UserIsLikes { get; set; } = false;
    }
}
