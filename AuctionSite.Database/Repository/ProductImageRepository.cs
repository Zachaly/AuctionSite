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

        public Task AddProductImagesAsync(IEnumerable<ProductImage> images)
        {
            _dbContext.ProductImage.AddRangeAsync(images);

            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteProductImageByIdAsync(int id)
        {
            var image = _dbContext.ProductImage.FirstOrDefault(x => x.Id == id);

            _dbContext.ProductImage.Remove(image);

            return _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetProductImageById<T>(int id, Func<ProductImage, T> selector)
            => _dbContext.ProductImage.Where(image => image.Id == id).Select(selector).FirstOrDefault();

        public IEnumerable<T> GetProductImagesByProductId<T>(int productId, Func<ProductImage, T> selector)
            => _dbContext.ProductImage.Where(image => image.ProductId == productId).Select(selector);
    }
}
