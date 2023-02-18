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
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderStock> OrderStock { get; set; }
        public DbSet<SaveList> SaveList { get; set; }
        public DbSet<ListStock> ListStock { get; set; }
        public DbSet<ProductReview> ProductReview { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }

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

            builder.Entity<ApplicationUser>()
                .HasOne(user => user.Cart)
                .WithOne(cart => cart.User)
                .HasForeignKey<Cart>(cart => cart.UserId);

            builder.Entity<Stock>()
                .HasMany(stock => stock.StocksOnHold)
                .WithOne(stock => stock.Stock)
                .HasForeignKey(stock => stock.StockId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Stock>()
                .HasMany(stock => stock.OrderStocks)
                .WithOne(stock => stock.Stock)
                .HasForeignKey(stock => stock.StockId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Stock>()
                .HasMany(stock => stock.ListStocks)
                .WithOne(stock => stock.Stock)
                .HasForeignKey(stock => stock.StockId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Product>()
                .HasMany(product => product.Reviews)
                .WithOne(review => review.Product)
                .HasForeignKey(review => review.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Product>()
                .HasOne(product => product.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
