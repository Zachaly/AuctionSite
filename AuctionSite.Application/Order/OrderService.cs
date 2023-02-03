using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Order;
using AuctionSite.Models.Order.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Application
{
    [Implementation(typeof(IOrderService))]
    public class OrderService : IOrderService
    {
        private readonly IOrderFactory _orderFactory;
        private readonly IOrderRepository _orderRepository;
        private readonly IResponseFactory _responseFactory;
        private readonly IStockOnHoldRepository _stockOnHoldRepository;

        public OrderService(IOrderFactory orderFactory, IOrderRepository orderRepository,
            IResponseFactory responseFactory, IStockOnHoldRepository stockOnHoldRepository)
        {
            _orderFactory = orderFactory;
            _orderRepository = orderRepository;
            _responseFactory = responseFactory;
            _stockOnHoldRepository = stockOnHoldRepository;
        }

        public Task<ResponseModel> AddOrderAsync(AddOrderRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponseModel<OrderModel>> GetOrderByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponseModel<IEnumerable<OrderListItem>>> GetOrdersByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
