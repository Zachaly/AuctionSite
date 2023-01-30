namespace AuctionSite.Models.Response
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public IDictionary<string, string[]>? ValidationErrors { get; set; }
        public int? NewEntityId { get; set; }
    }
}
