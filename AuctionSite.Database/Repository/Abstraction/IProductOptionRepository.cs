using AuctionSite.Domain.Entity;

namespace AuctionSite.Database.Repository.Abstraction
{
    public interface IProductOptionRepository
    {
        Task AddProductOptionAsync(ProductOption option);
        Task DeleteProductOptionByIdAsync(int id);
    }
}
