﻿using Microsoft.AspNetCore.Identity;

namespace AuctionSite.Domain.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public UserInfo? Info { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
