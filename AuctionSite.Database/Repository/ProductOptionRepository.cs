using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;

namespace AuctionSite.Database.Repository
{
    [Implementation(typeof(IProductOptionRepository))]
    public class ProductOptionRepository : IProductOptionRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductOptionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddProductOptionAsync(ProductOption option)
        {
            _dbContext.ProductOption.AddAsync(option);

            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteProductOptionByIdAsync(int id)
        {
            _dbContext.ProductOption.Remove(_dbContext.ProductOption.Find(id));

            return _dbContext.SaveChangesAsync();
        }
    }
}
