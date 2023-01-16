namespace AuctionSite.Models.Response
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public Dictionary<string, string>? ValidationErrors { get; set; }
    }
}
