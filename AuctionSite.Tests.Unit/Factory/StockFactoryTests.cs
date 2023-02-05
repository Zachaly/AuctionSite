using AuctionSite.Application;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.Stock.Request;

namespace AuctionSite.Tests.Unit.Factory
{
    public class StockFactoryTests
    {
        private readonly StockFactory _factory;

        public StockFactoryTests()
        {
            _factory = new StockFactory();
        }

        [Fact]
        public void Create_WithProductId()
        {
            var request = new AddStockRequest
            {
                ProductId = 1,
                Quantity = 2,
                Value = "val"
            };

            var stock = _factory.Create(request);

            Assert.Equal(request.ProductId, stock.ProductId);
            Assert.Equal(request.Value, stock.Value);
            Assert.Equal(request.Quantity, stock.Quantity);
        }

        [Fact]
        public void CreateModel()
        {
            var stock = new Stock
            {
                Id = 1,
                Quantity = 2,
                Value = "val"
            };

            var model = _factory.CreateModel(stock);

            Assert.Equal(stock.Id, model.Id);
            Assert.Equal(stock.Quantity, model.Quantity);
            Assert.Equal(stock.Value, model.Value);
        }

        [Fact]
        public void Create_WithoutProductId()
        {
            var request = new AddStockRequest
            {
                Quantity = 1,
                Value = "val"
            };

            var stock = _factory.Create(request);

            Assert.Equal(request.Value, stock.Value);
            Assert.Equal(request.Quantity, stock.Quantity);
            Assert.Equal(0, stock.ProductId);
        }
    }
}
