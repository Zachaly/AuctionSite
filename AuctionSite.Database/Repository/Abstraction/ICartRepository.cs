using AuctionSite.Domain.Entity;

namespace AuctionSite.Database.Repository.Abstraction
{
    public interface ICartRepository
    {
        Task AddCartAsync(Cart cart);
        Task DeleteCartByIdAsync(int id);
        T GetCartByUserId<T>(string id, Func<Cart, T> selector);
        int GetCartItemsCountByUserId(string userId);
    }
}
