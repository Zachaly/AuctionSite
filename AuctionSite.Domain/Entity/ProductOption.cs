namespace AuctionSite.Domain.Entity
{
    public class ProductOption
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Value { get; set; }
        public int Quantity { get; set; }
    }
}
