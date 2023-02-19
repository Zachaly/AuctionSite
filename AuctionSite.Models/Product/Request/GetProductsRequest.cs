namespace AuctionSite.Models.Product.Request
{
    public class GetProductsRequest : PagedRequest
    {
        public string? UserId { get; set; }
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
    }
}
