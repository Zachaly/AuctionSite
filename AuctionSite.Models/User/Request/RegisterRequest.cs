using AuctionSite.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSite.Models.User.Request
{
    public class RegisterRequest
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
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
