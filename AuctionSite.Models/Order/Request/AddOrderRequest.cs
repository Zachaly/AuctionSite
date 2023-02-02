namespace AuctionSite.Models.Order.Request
{
    public class AddOrderRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PaymentId { get; set; }
        public string UserId { get; set; }
        public IEnumerable<int> StockIds { get; set; }
    }
}
