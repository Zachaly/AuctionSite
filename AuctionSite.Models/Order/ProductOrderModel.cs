using AuctionSite.Domain.Enum;

namespace AuctionSite.Models.Order
{
    public class ProductOrderModel
    {
        public int StockId { get; set; }
        public int StockName { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string? PaymentId { get; set; }
        public DateTime CreationDate { get; set; }
        public RealizationStatus Status { get; set; }
    }
}
