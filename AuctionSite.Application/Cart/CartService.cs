using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Cart;
using AuctionSite.Models.Cart.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Application
{
    [Implementation(typeof(ICartService))]
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartFactory _cartFactory;
        private readonly IStockOnHoldFactory _stockOnHoldFactory;
        private readonly IStockOnHoldRepository _stockOnHoldRepository;
        private readonly IResponseFactory _responseFactory;

        public CartService(ICartRepository cartRepository, ICartFactory cartFactory,
            IStockOnHoldFactory stockOnHoldFactory, IStockOnHoldRepository stockOnHoldRepository,
            IResponseFactory responseFactory)
        {
            _cartRepository = cartRepository;
            _cartFactory = cartFactory;
            _stockOnHoldFactory = stockOnHoldFactory;
            _stockOnHoldRepository = stockOnHoldRepository;
            _responseFactory = responseFactory;
        }

        public Task<ResponseModel> AddCartAsync(AddCartRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> AddToCartAsync(AddToCartRequest request)
        {
            throw new NotImplementedException();
        }

        public DataResponseModel<CartModel> GetCartByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public DataResponseModel<int> GetCartItemsCount(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> RemoveFromCartAsync(int stockId)
        {
            throw new NotImplementedException();
        }
    }
}
