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
        Task<ResponseModel> MoveRealizationStatus(MoveRealizationStatusRequest request);
        Task<DataResponseModel<IEnumerable<OrderManagementItem>>> GetProductOrders(int productId);
        Task<DataResponseModel<OrderProductModel>> GetOrderStockById(int id);
    }
}
