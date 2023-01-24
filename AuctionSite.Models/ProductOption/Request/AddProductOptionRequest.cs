namespace AuctionSite.Models.ProductOption.Request
{
    public class AddProductOptionRequest
    {
        public int? ProductId { get; set; }
        public string Value { get; set; }
        public int Quantity { get; set; }
    }
}
