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
        public ProductOptionModel CreateModel(ProductOption option)
            => new ProductOptionModel
            {
                Id = option.Id,
                Quantity = option.Quantity,
                Value = option.Value,
            };

        public ProductOption Create(AddProductOptionRequest request)
            => new ProductOption
            {
                ProductId = request.ProductId.GetValueOrDefault(),
                Quantity = request.Quantity,
                Value = request.Value,
            };
    }
}
