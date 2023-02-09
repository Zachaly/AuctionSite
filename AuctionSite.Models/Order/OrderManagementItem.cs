using AuctionSite.Domain.Enum;

namespace AuctionSite.Models.Order
{
    public class OrderManagementItem
    {
        public int OrderStockId { get; set; }
        public string StockName { get; set; }
        public int Quantity { get; set; }
        public RealizationStatus Status { get; set; }
    }
}
