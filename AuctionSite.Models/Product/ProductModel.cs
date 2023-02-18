using AuctionSite.Models.Stock;

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
        public string StockName { get; set; }
        public IEnumerable<StockModel> Stocks { get; set; }
        public IEnumerable<int> ImageIds { get; set; }
        public string Created { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
