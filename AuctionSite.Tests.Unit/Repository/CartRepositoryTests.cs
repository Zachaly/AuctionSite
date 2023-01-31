using AuctionSite.Database.Repository;
using AuctionSite.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSite.Tests.Unit.Repository
{
    public class CartRepositoryTests : DatabaseTest
    {
        private readonly CartRepository _repository;

        public CartRepositoryTests() : base()
        {
            _repository = new CartRepository(_dbContext);
        }

        [Fact]
        public async Task AddCartAsync()
        {
            var cart = new Cart
            {
                UserId = "user",
            };

            await _repository.AddCartAsync(cart);

            Assert.Contains(_dbContext.Cart, x => x.UserId == cart.UserId);
        }

        [Fact]
        public async Task DeleteCartByIdAsync()
        {
            const int CartId = 3;

            AddContent(new List<Cart>
            {
                new Cart { Id = 1, UserId = "user1" },
                new Cart { Id = 2, UserId = "user2" },
                new Cart { Id = CartId, UserId = "user3" },
                new Cart { Id = 4, UserId = "user4" },
                new Cart { Id = 5, UserId = "user5" },
            });

            await _repository.DeleteCartByIdAsync(CartId);

            Assert.DoesNotContain(_dbContext.Cart, x => x.Id == CartId);
        }

        [Fact]
        public void GetCartByUserId()
        {
            const string Id = "user";
            AddContent(new List<Cart>
            {
                new Cart { Id = 1, UserId = "user1" },
                new Cart { Id = 2, UserId = "user2" },
                new Cart { Id = 3, UserId = Id },
                new Cart { Id = 4, UserId = "user4" },
                new Cart { Id = 5, UserId = "user5" },
            });

            var res = _repository.GetCartByUserId(Id, x => x);

            Assert.NotNull(res);
            Assert.NotNull(res.StocksOnHold);
        }

        [Fact]
        public void GetCartItemsCountByUserId()
        {
            const string Id = "user";

            AddContent(new List<Stock>
            {
                new Stock
                {
                    Id = 1,
                    Value = "val",
                    Product = new Product
                    {
                        OwnerId = "id",
                        StockName = "stock",
                        Price = 123,
                        Name = "prod",
                        Description = "desc"
                    },
                    Quantity = 10
                }
            });

            AddContent(new List<Cart>
            {
                new Cart { Id = 1, UserId = "user1" },
                new Cart { Id = 2, UserId = "user2" },
                new Cart 
                { 
                    Id = 3,
                    UserId = Id,
                    StocksOnHold = new List<StockOnHold> 
                    { 
                        new StockOnHold { Quantity = 5, CartId = 3, StockId = 1 },
                        new StockOnHold { Quantity = 5, CartId = 3, StockId = 1 },
                        new StockOnHold { Quantity = 5, CartId = 3, StockId = 1 },
                        new StockOnHold { Quantity = 5, CartId = 3, StockId = 1 },
                    }
                },
                new Cart { Id = 4, UserId = "user4" },
                new Cart { Id = 5, UserId = "user5" },
            });

            var res = _repository.GetCartItemsCountByUserId(Id);

            Assert.Equal(4, res);
        }
    }
}
