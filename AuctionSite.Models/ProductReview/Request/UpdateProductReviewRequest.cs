namespace AuctionSite.Models.ProductReview.Request
{
    public class UpdateProductReviewRequest
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int? Score { get; set; }
    }
}
