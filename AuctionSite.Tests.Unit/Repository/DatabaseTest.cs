using AuctionSite.Database;
using Microsoft.EntityFrameworkCore;

namespace AuctionSite.Tests.Unit.Repository
{
    public class DatabaseTest
    {
        protected AppDbContext _dbContext;

        public DatabaseTest() 
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _dbContext = new AppDbContext(options);
        }

        protected void AddContent<T>(List<T> content) where T : class
        {
            _dbContext.Set<T>().AddRange(content);
            _dbContext.SaveChanges();
        }

        protected void AddContent<T>(T content) where T : class
        {
            _dbContext.Set<T>().Add(content);
            _dbContext.SaveChanges();
        }
    }
}
