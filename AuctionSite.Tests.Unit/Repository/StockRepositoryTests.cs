using AuctionSite.Database.Repository;
using AuctionSite.Domain.Entity;

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
    }
}
