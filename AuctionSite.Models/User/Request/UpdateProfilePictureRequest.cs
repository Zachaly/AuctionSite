using Microsoft.AspNetCore.Http;

namespace AuctionSite.Models.User.Request
{
    public class UpdateProfilePictureRequest
    {
        public string UserId { get; set; }
        public IFormFile? File { get; set; }
    }
}
