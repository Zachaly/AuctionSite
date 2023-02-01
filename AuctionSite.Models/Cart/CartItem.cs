namespace AuctionSite.Models.Cart
{
    public class CartItem
    {
        public int StockOnHoldId { get; set; }
        public string Value { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
