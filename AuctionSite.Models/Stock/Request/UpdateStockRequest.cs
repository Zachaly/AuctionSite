namespace AuctionSite.Models.Stock.Request
{
    public class UpdateStockRequest
    {
        public int Id { get; set; }
        public string? Value { get; set; }
        public int? Quantity { get; set; }
    }
}
