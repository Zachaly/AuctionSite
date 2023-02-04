namespace AuctionSite.Models.Order
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string Created { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string PaymentId { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }
}
