namespace AuctionSite.Models.ListStock.Request
{
    public class AddListStockRequest
    {
        public int ListId { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
    }
}
