using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Order;
using AuctionSite.Models.Order.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Application
{
    [Implementation(typeof(IOrderService))]
    public class OrderService : IOrderService
    {


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
