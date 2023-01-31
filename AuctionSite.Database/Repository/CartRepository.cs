using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using Microsoft.EntityFrameworkCore;

namespace AuctionSite.Database.Repository
{
    [Implementation(typeof(ICartRepository))]
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _dbContext;

        public CartRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddCartAsync(Cart cart)
        {
            _dbContext.Cart.Add(cart);

            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteCartByIdAsync(int id)
        {
            _dbContext.Cart.Remove(_dbContext.Cart.Find(id));

            return _dbContext.SaveChangesAsync();
        }

        public T GetCartByUserId<T>(string userId, Func<Cart, T> selector)
            => _dbContext.Cart
                .Include(cart => cart.StocksOnHold)
                .ThenInclude(stock => stock.Stock)
                .ThenInclude(stock => stock.Product)
                .Where(cart => cart.UserId == userId)
                .Select(selector)
                .FirstOrDefault();

        public int GetCartItemsCountByUserId(string userId)
            => _dbContext.Cart
                .Include(cart => cart.StocksOnHold)
                .FirstOrDefault(cart => cart.UserId == userId)?.StocksOnHold.Count ?? 0;
    }
}
