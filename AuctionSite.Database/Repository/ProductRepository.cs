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

        public int GetCategoryPageCount(int categoryId, int pageSize)
        {
            var count = (decimal)_dbContext.Product.Count(product => product.CategoryId == categoryId);

            return (int)Math.Ceiling(count / pageSize);
        }

        public int GetPageCount(int pageSize)
        {
            var count = (decimal)_dbContext.Product.Count();

            return (int)Math.Ceiling(count / pageSize);
        }

        public int GetPageCount(int? categoryId, string? name, int pageSize)
        {
            throw new NotImplementedException();
        }

        public T GetProductById<T>(int id, Func<Product, T> selector)
            => _dbContext.Product.Include(product => product.Owner)
                .Include(product => product.Stocks)
                .Include(product => product.Images)
                .Include(product => product.Category)
                .Where(product => product.Id == id)
                .Select(selector)
                .FirstOrDefault();

        public IEnumerable<T> GetProducts<T>(int pageIndex, int pageSize, Func<Product, T> selector)
            => _dbContext.Product
                .Include(product => product.Images)
                .OrderByDescending(product => product.Created)
                .Skip(pageIndex * pageSize)
                .Take(pageSize).Select(selector);

        public IEnumerable<T> GetProductsByCategoryId<T>(int categoryId, int pageIndex, int pageSize, Func<Product, T> selector)
            => _dbContext.Product
                .Include(product => product.Images)
                .Where(product => product.CategoryId == categoryId)
                .OrderByDescending(product => product.Created)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Select(selector);

        public IEnumerable<T> GetProductsByUserId<T>(string id, int pageSize, int pageIndex, Func<Product, T> selector)
            => _dbContext.Product
                .Include(product => product.Images)
                .Where(product => product.OwnerId == id)
                .OrderByDescending(product => product.Created)
                .Skip(pageIndex * pageSize)
                .Take(pageSize).Select(selector);

        public int GetUserPageCount(string userId, int pageSize)
        {
            var count = (decimal)_dbContext.Product.Where(product => product.OwnerId == userId).Count();

            return (int)Math.Ceiling(count / pageSize);
        }

        public IEnumerable<T> SearchProducts<T>(int? categoryId, string? name, int pageIndex, int pageSize, Func<Product, T> selector)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(Product product)
        {
            _dbContext.Product.Update(product);

            return _dbContext.SaveChangesAsync();
        }
    }
}
