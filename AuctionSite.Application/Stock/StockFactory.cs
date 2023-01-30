using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Stock;
using AuctionSite.Models.Stock.Request;

namespace AuctionSite.Application
{
    [Implementation(typeof(IStockFactory))]
    public class StockFactory : IStockFactory
    {
        public StockModel CreateModel(Stock stock)
            => new StockModel
            {
                Id = stock.Id,
                Quantity = stock.Quantity,
                Value = stock.Value,
            };

        public Stock Create(AddStockRequest request)
            => new Stock
            {
                ProductId = request.ProductId.GetValueOrDefault(),
                Quantity = request.Quantity,
                Value = request.Value,
            };
    }
}
