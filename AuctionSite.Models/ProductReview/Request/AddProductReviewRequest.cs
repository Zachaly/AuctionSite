namespace AuctionSite.Models.ProductReview.Request
{
    public class AddProductReviewRequest
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public int Score { get; set; }
    }
}
