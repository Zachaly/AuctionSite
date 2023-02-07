using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using AuctionSite.Models.ListStock;
using AuctionSite.Models.ListStock.Request;

namespace AuctionSite.Application
{
    [Implementation(typeof(IListStockFactory))]
    public class ListStockFactory : IListStockFactory
    {
        public ListStock Create(AddListStockRequest request)
            => new ListStock
            {
                ListId = request.ListId,
                Quantity = request.Quantity,
                StockId = request.StockId,
            };

        public ListStockModel CreateModel(ListStock stock)
            => new ListStockModel
            {
                Id = stock.Id,
                Price = stock.Stock.Product.Price,
                ProductId = stock.Stock.ProductId,
                ProductName = stock.Stock.Product.Name,
                Quantity = stock.Quantity,
                StockValue = stock.Stock.Value
            };
    }
}
