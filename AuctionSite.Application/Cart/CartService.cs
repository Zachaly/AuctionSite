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

        public async Task<ResponseModel> AddCartAsync(AddCartRequest request)
        {
            try
            {
                var cart = _cartRepository.GetCartByUserId(request.UserId, x => x);

                if(cart is not null)
                {
                    return _responseFactory.CreateSuccess();
                }

                cart = _cartFactory.Create(request);

                await _cartRepository.AddCartAsync(cart);

                return _responseFactory.CreateSuccess();

            }
            catch(Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }

        public async Task<ResponseModel> AddToCartAsync(AddToCartRequest request)
        {
            try
            {
                var cart = _cartRepository.GetCartByUserId(request.UserId, x => x);

                if(cart is null)
                {
                    return _responseFactory.CreateFailure("Cart not found");
                }

                var stock = _stockOnHoldFactory.Create(request, cart.Id);

                await _stockOnHoldRepository.AddStockOnHoldAsync(stock);

                return _responseFactory.CreateSuccess();
            }
            catch(Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }

        public DataResponseModel<CartModel> GetCartByUserId(string userId)
        {
            var cart = _cartRepository.GetCartByUserId(userId, x => _cartFactory.CreateModel(x));

            if(cart is null)
            {
                return _responseFactory.CreateFailure<CartModel>("Cart not found");
            }

            return _responseFactory.CreateSuccess(cart);
        }

        public DataResponseModel<int> GetCartItemsCount(string userId)
        {
            var count = _cartRepository.GetCartItemsCountByUserId(userId);

            return _responseFactory.CreateSuccess(count);
        }

        public async Task<ResponseModel> RemoveFromCartAsync(int stockId)
        {
            try
            {
                await _stockOnHoldRepository.DeleteStockOnHoldByIdAsync(stockId);

                return _responseFactory.CreateSuccess();
            }
            catch(Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }
    }
}
