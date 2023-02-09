using AuctionSite.Domain.Enum;

namespace AuctionSite.Domain.Entity
{
    public class OrderStock
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
        public RealizationStatus RealizationStatus { get; set; }
    }
}
