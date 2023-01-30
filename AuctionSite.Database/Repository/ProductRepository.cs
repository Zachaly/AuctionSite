using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using Microsoft.EntityFrameworkCore;

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
            _dbContext.Product.Add(product);

            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteProductByIdAsync(int id)
        {
            _dbContext.Product.Remove(_dbContext.Product.Find(id));

            return _dbContext.SaveChangesAsync();
        }

        public int GetPageCount(int pageSize)
        {
            var count = (decimal)_dbContext.Product.Count();

            return (int)Math.Ceiling(count / pageSize);
        }

        public T GetProductById<T>(int id, Func<Product, T> selector)
            => _dbContext.Product.Include(product => product.Owner)
                .Include(product => product.Stocks)
                .Include(product => product.Images)
                .Where(product => product.Id == id)
                .Select(selector)
                .FirstOrDefault();

        public IEnumerable<T> GetProducts<T>(int pageIndex, int pageSize, Func<Product, T> selector)
            => _dbContext.Product
                .Include(product => product.Images)
                .Skip(pageIndex * pageSize)
                .Take(pageSize).Select(selector);
    }
}
