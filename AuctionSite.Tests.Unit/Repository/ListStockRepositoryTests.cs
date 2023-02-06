using AuctionSite.Database.Repository;
using AuctionSite.Domain.Entity;

namespace AuctionSite.Tests.Unit.Repository
{
    public class ListStockRepositoryTests : DatabaseTest
    {
        private readonly ListStockRepository _repository;

        public ListStockRepositoryTests() : base()
        {
            _repository = new ListStockRepository(_dbContext);
        }

        [Fact]
        public async Task AddListStockAsync()
        {
            var stock = new ListStock
            {
                ListId = 1,
                Quantity = 2,
                StockId = 3,
            };

            await  _repository.AddListStockAsync(stock);

            Assert.Contains(_dbContext.ListStock, x => x.Quantity == stock.Quantity &&
                x.StockId == stock.StockId && 
                x.ListId == stock.ListId);
        }

        [Fact]
        public async Task DeleteListStockByIdAsync()
        {
            var stock = new List<ListStock>
            {
                new ListStock { Id = 1, Quantity= 2, StockId = 3 },
                new ListStock { Id = 2, Quantity= 2, StockId = 3 },
                new ListStock { Id = 3, Quantity= 2, StockId = 3 },
            };

            AddContent(stock);

            const int Id = 2;

            await _repository.DeleteListStokcByIdAsync(Id);

            Assert.DoesNotContain(_dbContext.ListStock, x => x.Id == Id);
        }
    }
}
