namespace AuctionSite.Models.Stock.Request
{
    public class AddStockRequest
    {
        public int? ProductId { get; set; }
        public string Value { get; set; }
        public int Quantity { get; set; }
    }
}
