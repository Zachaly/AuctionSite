using AuctionSite.Domain.Entity;

namespace AuctionSite.Database.Repository.Abstraction
{
    public interface IProductRepository
    {
        IEnumerable<T> GetProducts<T>(int pageIndex, int pageSize, Func<Product, T> selector);
        IEnumerable<T> GetProductsByUserId<T>(string id, int pageSize, int pageIndex, Func<Product, T> selector);
        T GetProductById<T>(int id, Func<Product, T> selector);
        Task AddProductAsync(Product product);
        Task DeleteProductByIdAsync(int id);
        int GetPageCount(int pageSize);
        int GetUserPageCount(string userId, int pageSize);
    }
}
