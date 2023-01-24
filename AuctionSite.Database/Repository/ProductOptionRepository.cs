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
            throw new NotImplementedException();
        }

        public Task DeleteProductOptionByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
