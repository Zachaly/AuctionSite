namespace AuctionSite.Models.Payment.Request
{
    public class AddPaymentRequest
    {
        public string CustomerId { get; set; }
        public string Email { get; set; }
        public long Amount { get; set; }
    }
}
