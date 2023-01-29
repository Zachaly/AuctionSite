namespace AuctionSite.Domain.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
        public string OptionName { get; set; }
        public ICollection<ProductOption> Options { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<ProductImage> Images { get; set; }
    }
}
