using AuctionSite.Domain.Enum;

namespace AuctionSite.Models.Order
{
    public class OrderItem
    {
        public int OrderStockId { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public string StockName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }  
        public RealizationStatus Status { get; set; }
    }
}
