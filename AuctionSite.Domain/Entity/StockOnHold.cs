namespace AuctionSite.Domain.Entity
{
    public class StockOnHold
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
