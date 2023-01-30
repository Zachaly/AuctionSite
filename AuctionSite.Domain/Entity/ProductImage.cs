namespace AuctionSite.Domain.Entity
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
