using AuctionSite.Domain.Entity;

namespace AuctionSite.Database.Repository.Abstraction
{
    public interface IProductRepository
    {
        IEnumerable<T> GetProducts<T>(int pageIndex, int pageSize, Func<Product, T> selector);
        T GetProductById<T>(int id, Func<Product, T> selector);
        Task AddProductAsync(Product product);
        Task DeleteProductByIdAsync(int id);
    }
}
