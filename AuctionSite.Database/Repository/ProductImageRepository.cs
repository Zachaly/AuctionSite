using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;

namespace AuctionSite.Database.Repository
{
    [Implementation(typeof(IProductImageRepository))]
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductImageRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public Task AddProductImagesAsync(List<ProductImage> images)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetProductImageById<T>(int id, Func<ProductImage, T> selector)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetProductImagesByProductId<T>(int productId, Func<ProductImage, T> selector)
        {
            throw new NotImplementedException();
        }
    }
}
