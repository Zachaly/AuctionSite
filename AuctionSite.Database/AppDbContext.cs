using AuctionSite.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuctionSite.Database
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<StockOnHold> StockOnHold { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasOne(user => user.Info).WithOne(info => info.User)
                .HasForeignKey<UserInfo>(info => info.UserId);

            builder.Entity<UserInfo>()
                .HasKey(info => info.UserId);
        }
    }
}
