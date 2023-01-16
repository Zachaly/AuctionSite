namespace AuctionSite.Models.User.Response
{
    public class LoginResponse
    {
        public string AuthToken { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
