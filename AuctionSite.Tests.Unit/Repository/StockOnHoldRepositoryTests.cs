using AuctionSite.Database.Repository;
using AuctionSite.Domain.Entity;

namespace AuctionSite.Tests.Unit.Repository
{
    public class StockOnHoldRepositoryTests : DatabaseTest
    {
        private readonly StockOnHoldRepository _repository;

        public StockOnHoldRepositoryTests() : base()
        {
            _repository = new StockOnHoldRepository(_dbContext);
        }

        [Fact]
        public async Task AddStockOnHoldAsync_ValidQuantity()
        {
            const int OriginalQuantity = 10;

            var stock = new Stock
            {
                Id = 1,
                Quantity = OriginalQuantity,
                Value = "val"
            };

            AddContent(new List<Stock> { stock });

            var stockOnHold = new StockOnHold { Quantity = 6, CartId = 1, StockId = stock.Id };

            await _repository.AddStockOnHoldAsync(stockOnHold);

            Assert.Equal(OriginalQuantity - stockOnHold.Quantity, stock.Quantity);
            Assert.Contains(_dbContext.StockOnHold, x => x.Quantity == stockOnHold.Quantity);
        }

        [Fact]
        public async Task AddStockOnHoldAsync_QuantityExceedsMaximum()
        {
            const int OriginalQuantity = 10;

            var stock = new Stock
            {
                Id = 1,
                Quantity = OriginalQuantity,
                Value = "val"
            };

            AddContent(new List<Stock> { stock });

            var stockOnHold = new StockOnHold { Quantity = 11, CartId = 1, StockId = stock.Id };

            await _repository.AddStockOnHoldAsync(stockOnHold);

            Assert.Equal(OriginalQuantity, stock.Quantity);
            Assert.DoesNotContain(_dbContext.StockOnHold, x => x.Quantity == stockOnHold.Quantity);
        }

        [Fact]
        public async Task RemoveStockOnHoldByIdAsync()
        {
            const int OriginalQuantity = 10;

            var stock = new Stock
            {
                Id = 1,
                Quantity = OriginalQuantity,
                Value = "val"
            };

            AddContent(new List<Stock> { stock });

            var stocksOnHold = new List<StockOnHold>
            {
                new StockOnHold { Id = 1, StockId = stock.Id, Quantity = 3 },
                new StockOnHold { Id = 2, StockId = stock.Id, Quantity = 6 },
                new StockOnHold { Id = 3, StockId = stock.Id, Quantity = 9 },
                new StockOnHold { Id = 4, StockId = stock.Id, Quantity = 12 },
            };

            AddContent(stocksOnHold);

            const int Id = 3;

            await _repository.DeleteStockOnHoldByIdAsync(Id);

            Assert.DoesNotContain(_dbContext.StockOnHold, x => x.Id == Id);
            Assert.Equal(OriginalQuantity + stocksOnHold.FirstOrDefault(x => x.Id == Id).Quantity, stock.Quantity);
        }

        [Fact]
        public async Task GetStockOnHoldById()
        {
            const int OriginalQuantity = 10;

            var stock = new Stock
            {
                Id = 1,
                Quantity = OriginalQuantity,
                Value = "val"
            };

            AddContent(new List<Stock> { stock });

            var stocksOnHold = new List<StockOnHold>
            {
                new StockOnHold { Id = 1, StockId = stock.Id, Quantity = 3 },
                new StockOnHold { Id = 2, StockId = stock.Id, Quantity = 6 },
                new StockOnHold { Id = 3, StockId = stock.Id, Quantity = 9 },
                new StockOnHold { Id = 4, StockId = stock.Id, Quantity = 12 },
            };

            AddContent(stocksOnHold);

            const int Id = 3;

            var res = _repository.GetStockOnHoldById(Id, x => x);

            var testStock = _dbContext.StockOnHold.FirstOrDefault(x => x.Id == Id);

            Assert.Equal(testStock.Id, res.Id);
            Assert.Equal(testStock.Quantity, res.Quantity);
        }

        [Fact]
        public async Task GetStocksOnHoldByCartId()
        {
            const int OriginalQuantity = 10;

            var stock = new Stock
            {
                Id = 1,
                Quantity = OriginalQuantity,
                Value = "val"
            };

            var cart = new Cart
            {
                Id = 1,
                UserId = "usr"
            };

            AddContent(new List<Stock> { stock });
            AddContent(new List<Cart> { cart });

            var stocksOnHold = new List<StockOnHold>
            {
                new StockOnHold { Id = 1, StockId = stock.Id, Quantity = 3, CartId = cart.Id },
                new StockOnHold { Id = 2, StockId = stock.Id, Quantity = 6, CartId = 2 },
                new StockOnHold { Id = 3, StockId = stock.Id, Quantity = 9, CartId = cart.Id },
                new StockOnHold { Id = 4, StockId = stock.Id, Quantity = 12, CartId = 3 },
                new StockOnHold { Id = 5, StockId = stock.Id, Quantity = 3, CartId = cart.Id },
                new StockOnHold { Id = 6, StockId = stock.Id, Quantity = 6, CartId = 4 },
                new StockOnHold { Id = 7, StockId = stock.Id, Quantity = 9, CartId = cart.Id },
                new StockOnHold { Id = 8, StockId = stock.Id, Quantity = 12, CartId = 5 },
            };

            AddContent(stocksOnHold);

            var res = _repository.GetStocksOnHoldByCartId(cart.Id, x => x);

            Assert.Equivalent(res.Select(x => x.Id), stocksOnHold.Where(x => x.CartId == cart.Id).Select(x => x.Id), true);
        }
    }
}
