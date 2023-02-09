using AuctionSite.Models.Order;
using AuctionSite.Models.Order.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Application.Abstraction
{
    public interface IOrderService
    {
        Task<DataResponseModel<OrderModel>> GetOrderByIdAsync(int id);
        Task<DataResponseModel<IEnumerable<OrderListItem>>> GetOrdersByUserIdAsync(string userId);
        Task<ResponseModel> AddOrderAsync(AddOrderRequest request);
        Task<ResponseModel> MoveRealizationStatus(int orderStockId);
        Task<DataResponseModel<IEnumerable<OrderItem>>> GetProductOrders(int productId);
        Task<DataResponseModel<ProductOrderModel>> GetOrderStockById(int id);
    }
}
