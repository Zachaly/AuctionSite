namespace AuctionSite.Models.ListStock
{
    public class ListStockModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public string StockValue { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
