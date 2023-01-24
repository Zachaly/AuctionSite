using AuctionSite.Models.ProductOption.Request;

namespace AuctionSite.Models.Product.Request
{
    public class AddProductRequest
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OptionName { get; set; }
        public IEnumerable<AddProductOptionRequest> Options { get; set; }
        public decimal Price { get; set; }
    }
}
