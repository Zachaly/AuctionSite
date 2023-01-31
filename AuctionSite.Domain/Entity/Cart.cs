namespace AuctionSite.Domain.Entity
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<StockOnHold> StocksOnHold { get; set; }
    }
}
