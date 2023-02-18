namespace AuctionSite.Domain.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
        public string StockName { get; set; }
        public ICollection<Stock> Stocks { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public DateTime Created { get; set; }
        public ICollection<ProductReview> Reviews { get; set; }
        public int? CategoryId { get; set; }
        public ProductCategory? Category { get; set; }
    }
}
