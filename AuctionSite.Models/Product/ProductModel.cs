using AuctionSite.Models.ProductOption;

namespace AuctionSite.Models.Product
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string OptionName { get; set; }
        public IEnumerable<ProductOptionModel> Options { get; set; }
    }
}
