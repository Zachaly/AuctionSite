namespace AuctionSite.Models.ProductReview
{
    public class ProductReviewListModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public int Score { get; set; }
    }
}
