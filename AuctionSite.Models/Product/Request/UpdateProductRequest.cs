namespace AuctionSite.Models.Product.Request
{
    public class UpdateProductRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? StockName { get; set; }
        public decimal? Price { get; set; }
    }
}
