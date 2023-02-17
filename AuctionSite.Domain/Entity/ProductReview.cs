namespace AuctionSite.Domain.Entity
{
    public class ProductReview
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Content { get; set; }
        public int Score { get; set; }
    }
}
