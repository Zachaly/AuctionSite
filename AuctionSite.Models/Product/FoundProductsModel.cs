
namespace AuctionSite.Models.Product
{
    public class FoundProductsModel
    {
        public IEnumerable<ProductListItemModel> Products { get; set; }
        public int PageCount { get; set; }
    }
}
