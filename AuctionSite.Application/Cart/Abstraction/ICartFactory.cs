using AuctionSite.Domain.Entity;
using AuctionSite.Models.Cart;
using AuctionSite.Models.Cart.Request;

namespace AuctionSite.Application.Abstraction
{
    public interface ICartFactory
    {
        CartModel CreateModel(Cart cart);
        Cart Create(AddCartRequest request);
    }
}
