using AuctionSite.Application;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.Cart.Request;

namespace AuctionSite.Tests.Unit.Factory
{
    public class StockOnHoldFactoryTests
    {
        private readonly StockOnHoldFactory _factory;

        public StockOnHoldFactoryTests()
        {
            _factory = new StockOnHoldFactory();
        }

        [Fact]
        public void Create()
        {
            var request = new AddToCartRequest
            {
                Quantity = 1,
                StockId = 2,
                UserId = "id"
            };

            const int CartId = 3;

            var stock = _factory.Create(request, CartId);

            Assert.Equal(request.Quantity, stock.Quantity);
            Assert.Equal(request.StockId, stock.StockId);
            Assert.Equal(CartId, stock.CartId);
        }

        [Fact]
        public void CreateCartItem()
        {
            var stock = new StockOnHold
            {
                Id = 1,
                Quantity = 2,
                Stock = new Stock
                {
                    Value = "val",
                    ProductId = 1,
                    Product = new Product { Name = "name", Price = 123 }
                }
            };

            var item = _factory.CreateCartItem(stock);

            Assert.Equal(stock.Id, item.StockOnHoldId);
            Assert.Equal(stock.Quantity, item.Quantity);
            Assert.Equal(stock.Stock.Value, item.Value);
            Assert.Equal(stock.Stock.ProductId, item.ProductId);
            Assert.Equal(stock.Stock.Product.Name, item.ProductName);
            Assert.Equal(stock.Quantity * stock.Stock.Product.Price, item.Price);
        }
    }
}
