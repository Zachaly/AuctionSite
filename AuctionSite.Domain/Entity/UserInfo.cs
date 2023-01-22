using AuctionSite.Domain.Enum;

namespace AuctionSite.Domain.Entity
{
    public class UserInfo
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
    }
}
