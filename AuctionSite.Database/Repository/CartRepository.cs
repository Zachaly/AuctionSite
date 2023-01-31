using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;

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
            throw new NotImplementedException();
        }

        public Task DeleteCartByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public T GetCartByUserId<T>(string userId, Func<Cart, T> selector)
        {
            throw new NotImplementedException();
        }

        public int GetCartItemsCountByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
