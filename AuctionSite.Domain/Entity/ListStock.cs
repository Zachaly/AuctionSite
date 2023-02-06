namespace AuctionSite.Domain.Entity
{
    public class ListStock
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public SaveList List { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        public int Quantity { get; set; }
    }
}
