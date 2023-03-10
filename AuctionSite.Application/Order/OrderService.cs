using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Enum;
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

        public async Task<ResponseModel> AddOrderAsync(AddOrderRequest request)
        {
            try
            {
                var order = _orderFactory.Create(request);
                var stocks = _stockOnHoldRepository.GetStocksOnHoldByCartId(request.CartId, x => x);

                await _orderRepository.AddOrderAsync(order);

                await _orderRepository.AddOrderStocksAsync(
                    stocks.Select(stock => _orderFactory.CreateStock(stock, order.Id)),
                    stocks.Select(stock => stock.Id));

                return _responseFactory.CreateSuccess();
            }
            catch(Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }

        public Task<DataResponseModel<OrderModel>> GetOrderByIdAsync(int id)
        {
            var order = _orderRepository.GetOrderById(id, order => _orderFactory.CreateModel(order));

            if(order is null)
            {
                return Task.FromResult(_responseFactory.CreateFailure<OrderModel>("Order not found"));
            }

            return Task.FromResult(_responseFactory.CreateSuccess(order));
        }

        public Task<DataResponseModel<IEnumerable<OrderListItem>>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = _orderRepository.GetOrdersByUserId(userId, order => _orderFactory.CreateListItem(order));

            return Task.FromResult(_responseFactory.CreateSuccess(orders));
        }

        public async Task<DataResponseModel<OrderProductModel>> GetOrderStockById(int id)
        {
            var data = _orderRepository.GetOrderStockByIdAsync(id, stock => _orderFactory.CreateModel(stock));

            return _responseFactory.CreateSuccess(data);
        }

        public async Task<DataResponseModel<IEnumerable<OrderManagementItem>>> GetProductOrders(int productId)
        {
            var data = _orderRepository.GetProductOrderStocks(productId, stock => _orderFactory.CreateManagementItem(stock));

            return _responseFactory.CreateSuccess(data);
        }

        public async Task<ResponseModel> MoveRealizationStatus(MoveRealizationStatusRequest request)
        {
            try
            {
                var stock = _orderRepository.GetOrderStockByIdAsync(request.StockId, stock => stock);

                if(stock.RealizationStatus >= RealizationStatus.Delivered)
                {
                    return _responseFactory.CreateFailure("Order already delivered");
                }

                stock.RealizationStatus++;

                await _orderRepository.UpdateOrderStock(stock);

                return _responseFactory.CreateSuccess();
            }
            catch(Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }
    }
}
