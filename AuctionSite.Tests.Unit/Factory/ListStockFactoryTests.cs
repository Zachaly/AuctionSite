using AuctionSite.Application;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.ListStock.Request;

namespace AuctionSite.Tests.Unit.Factory
{
    public class ListStockFactoryTests
    {
        private readonly ListStockFactory _factory;

        public ListStockFactoryTests()
        {
            _factory = new ListStockFactory();
        }

        [Fact]
        public void Create()
        {
            var request = new AddListStockRequest
            {
                StockId = 1,
                ListId = 2,
                Quantity = 3,
            };

            var stock = _factory.Create(request);

            Assert.Equal(request.ListId, stock.ListId);
            Assert.Equal(request.StockId, stock.StockId);
            Assert.Equal(request.Quantity, stock.Quantity);
        }

        [Fact]
        public void CreateModel()
        {
            var stock = new ListStock
            {
                Id = 1,
                StockId = 2,
                Stock = new Stock
                {
                    Id = 2,
                    Product = new Product
                    {
                        Id = 3,
                        Name = "prod",
                        StockName = "stock"
                    },
                    Value = "val",
                    ProductId = 3,
                    
                },
                Quantity = 3,
            };

            var model = _factory.CreateModel(stock);

            Assert.Equal(stock.Id, model.Id);
            Assert.Equal(stock.Stock.Value, model.StockValue);
            Assert.Equal(stock.Quantity, model.Quantity);
            Assert.Equal(stock.Stock.Product.Name, model.ProductName);
            Assert.Equal(stock.Stock.Product.Price, model.Price);
            Assert.Equal(stock.Stock.ProductId, model.ProductId);
        }
    }
}
