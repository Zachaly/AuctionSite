using AuctionSite.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuctionSite.Database
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserInfo> UserInfo { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
