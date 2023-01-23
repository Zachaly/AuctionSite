using AuctionSite.Domain.Enum;

namespace AuctionSite.Models.User
{
    public class UserProfileModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
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
