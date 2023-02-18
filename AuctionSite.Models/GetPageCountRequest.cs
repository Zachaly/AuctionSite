namespace AuctionSite.Models
{
    public class GetPageCountRequest
    {
        public int? PageSize { get; set; }
        public string? UserId { get; set; }
        public int? CategoryId { get; set; }
    }
}
