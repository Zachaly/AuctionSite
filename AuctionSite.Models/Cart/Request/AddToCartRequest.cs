namespace AuctionSite.Models.Cart.Request
{
    public class AddToCartRequest
    {
        public string UserId { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
    }
}
