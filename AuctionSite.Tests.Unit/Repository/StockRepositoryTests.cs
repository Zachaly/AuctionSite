using AuctionSite.Database.Repository;
using AuctionSite.Domain.Entity;
using Microsoft.IdentityModel.Tokens;

namespace AuctionSite.Tests.Unit.Repository
{
    public class StockRepositoryTests : DatabaseTest
    {
        private readonly StockRepository _repository;

        public StockRepositoryTests() : base()
        {
            _repository = new StockRepository(_dbContext);
        }

        [Fact]
        public async Task AddStockAsync()
        {
            var stock = new Stock
            {
                ProductId = 1,
                Quantity = 2,
                Value = "val"
            };

            await _repository.AddStockAsync(stock);

            Assert.Contains(_dbContext.Stock, x => x.ProductId == stock.Id);
        }

        [Fact]
        public async Task DeleteStockByIdAsync()
        {
            AddContent(new List<Stock>
            {
                new Stock { Id = 1, Quantity = 2, ProductId = 3, Value = "val" },
                new Stock { Id = 2, Quantity = 2, ProductId = 3, Value = "val" },
                new Stock { Id = 3, Quantity = 2, ProductId = 3, Value = "val" },
                new Stock { Id = 4, Quantity = 2, ProductId = 3, Value = "val" },
            });

            const int Id = 3;

            await _repository.DeleteStockByIdAsync(Id);

            Assert.DoesNotContain(_dbContext.Stock, x => x.Id == Id);
        }

        [Fact]
        public async Task GetStockById()
        {
            var stock = new Stock { Id = 3, Quantity = 4, ProductId = 5, Value = "val3" };

            AddContent(new List<Stock>
            {
                new Stock { Id = 1, Quantity = 2, ProductId = 3, Value = "val" },
                new Stock { Id = 2, Quantity = 3, ProductId = 4, Value = "val" },
                stock,
                new Stock { Id = 4, Quantity = 5, ProductId = 6, Value = "val" },
            });

            var res = _repository.GetStockById(stock.Id, x => x);

            Assert.Equal(stock.Quantity, res.Quantity);
            Assert.Equal(stock.ProductId, res.ProductId);
            Assert.Equal(stock.Value, res.Value);
        }

        [Fact]
        public async Task UpdateStockAsync()
        {
            var stock = new Stock { Id = 2, Quantity = 5, Value = "val" };

            AddContent(stock);

            const int NewQuantity = 10;

            stock.Quantity = NewQuantity;

            await _repository.UpdateStockAsync(stock);

            Assert.Contains(_dbContext.Stock, x => x.Id == stock.Id && x.Quantity == NewQuantity);
        }
    }
}
