namespace AuctionSite.Domain.Entity
{
    public class Stock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Value { get; set; }
        public int Quantity { get; set; }
        public ICollection<StockOnHold> StocksOnHold { get; set; }
    }
}
