using AuctionSite.Application;
using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.Cart;
using AuctionSite.Models.Cart.Request;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSite.Tests.Unit.Factory
{
    public class CartFactoryTests
    {
        private readonly CartFactory _factory;

        public CartFactoryTests()
        {
            var stockOnHoldFactory = new Mock<IStockOnHoldFactory>();
            stockOnHoldFactory.Setup(x => x.CreateCartItem(It.IsAny<StockOnHold>()))
                .Returns((StockOnHold stock) => new CartItem { StockOnHoldId = stock.Id });

            _factory = new CartFactory(stockOnHoldFactory.Object);
        }

        [Fact]
        public void Create()
        {
            var request = new AddCartRequest
            {
                UserId = "id"
            };

            var cart = _factory.Create(request);
            
            Assert.Equal(request.UserId, cart.UserId);
        }

        [Fact]
        public void CreateModel()
        {
            var cart = new Cart
            {
                Id = 1,
                StocksOnHold = new List<StockOnHold>
                {
                    new StockOnHold { Id = 2 } ,
                    new StockOnHold { Id = 3 }
                },
                UserId = "id"
            };

            var model = _factory.CreateModel(cart);

            Assert.Equivalent(cart.StocksOnHold.Select(x => x.Id), model.Items.Select(x => x.StockOnHoldId));
            Assert.Equivalent(cart.Id, model.Id);
        }
    }
}
