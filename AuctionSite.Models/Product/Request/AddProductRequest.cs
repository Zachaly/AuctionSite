using AuctionSite.Models.Stock.Request;

namespace AuctionSite.Models.Product.Request
{
    public class AddProductRequest
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StockName { get; set; }
        public IEnumerable<AddStockRequest> Stocks { get; set; }
        public decimal Price { get; set; }
    }
}
