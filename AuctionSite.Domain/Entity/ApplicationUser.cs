using Microsoft.AspNetCore.Identity;

namespace AuctionSite.Domain.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string? ProfilePicture { get; set; }
        public UserInfo? Info { get; set; }
        public ICollection<Product> Products { get; set; }
        public int? CartId { get; set; }
        public Cart? Cart { get; set; }
        public ICollection<SaveList> Lists { get; set; }
    }
}
