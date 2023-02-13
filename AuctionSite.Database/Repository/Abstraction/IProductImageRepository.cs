using AuctionSite.Domain.Entity;

namespace AuctionSite.Database.Repository.Abstraction
{
    public interface IProductImageRepository
    {
        Task AddProductImagesAsync(IEnumerable<ProductImage> images);
        Task<T> GetProductImageById<T>(int id, Func<ProductImage, T> selector);
        IEnumerable<T> GetProductImagesByProductId<T>(int productId, Func<ProductImage, T> selector);
        Task DeleteProductImageByIdAsync(int id);
    }
}
