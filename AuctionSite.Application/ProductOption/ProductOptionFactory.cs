using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using AuctionSite.Models.ProductOption;
using AuctionSite.Models.ProductOption.Request;

namespace AuctionSite.Application
{
    [Implementation(typeof(IProductOptionFactory))]
    public class ProductOptionFactory : IProductOptionFactory
    {
        public ProductOption Create(AddProductOptionRequest request, int productId)
        {
            throw new NotImplementedException();
        }

        public ProductOptionModel CreateModel(ProductOption option)
        {
            throw new NotImplementedException();
        }

        public ProductOption Create(AddProductOptionRequest request)
        {
            throw new NotImplementedException();
        }

        public ProductOption CreateWithoutProductId(AddProductOptionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
