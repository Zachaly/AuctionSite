namespace AuctionSite.Domain.Util
{
    internal class Implementation : Attribute
    {
        public Type Interface { get; set; }

        public Implementation(Type @interface)
        {
            Interface = @interface;
        }
    }
}
