namespace AuctionSite.Models.Cart
{
    public class CartModel
    {
        public int Id { get; set; }
        public IEnumerable<CartItem> Items { get; set; }
    }
}
