namespace AuctionSite.Domain.Util
{
    public class Implementation : Attribute
    {
        public Type Interface { get; set; }

        public Implementation(Type @interface)
        {
            Interface = @interface;
        }
    }
}
