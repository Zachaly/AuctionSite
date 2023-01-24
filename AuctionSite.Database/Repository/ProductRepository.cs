using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;

namespace AuctionSite.Database.Repository
{
    [Implementation(typeof(IProductRepository))]
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public T GetProductById<T>(int id, Func<Product, T> selector)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetProducts<T>(int pageIndex, int pageSize, Func<Product, T> selector)
        {
            throw new NotImplementedException();
        }
    }
}
