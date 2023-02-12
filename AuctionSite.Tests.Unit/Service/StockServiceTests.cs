using AuctionSite.Application;
using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.Stock.Request;
using AuctionSite.Models.Response;
using Moq;

namespace AuctionSite.Tests.Unit.Service
{
    public class StockServiceTests : ServiceTest
    {
        private readonly Mock<IStockFactory> _stockFactory;
        private readonly Mock<IStockRepository> _stockRepository;
        private readonly StockService _service;

        public StockServiceTests() : base()
        {
            _stockFactory = new Mock<IStockFactory>();
            _stockRepository = new Mock<IStockRepository>();

            _service = new StockService(_responseFactory.Object, _stockFactory.Object, _stockRepository.Object);
        }

        [Fact]
        public async Task AddStockAsync_Success()
        {
            var stocks = new List<Stock>();

            _stockRepository.Setup(x => x.AddStockAsync(It.IsAny<Stock>()))
                .Callback((Stock stock) => stocks.Add(stock));

            _stockFactory.Setup(x => x.Create(It.IsAny<AddStockRequest>()))
                .Returns((AddStockRequest request) => new Stock
                {
                    ProductId = request.ProductId.GetValueOrDefault(),
                    Quantity = request.Quantity,
                    Value = request.Value
                });

            var request = new AddStockRequest
            {
                ProductId = 1,
                Quantity = 2,
                Value = "val"
            };

            var res = await _service.AddStockAsync(request);

            Assert.Contains(stocks, x => x.Value == request.Value);
            Assert.True(res.Success);
        }

        [Fact]
        public async Task AddStockAsync_ErrorThrown_Fail()
        {
            const string ErrorMessage = "Error";

            _stockRepository.Setup(x => x.AddStockAsync(It.IsAny<Stock>()))
                .Callback((Stock _) => throw new Exception(ErrorMessage));

            _stockFactory.Setup(x => x.Create(It.IsAny<AddStockRequest>()))
                .Returns((AddStockRequest request) => new Stock
                {
                    ProductId = request.ProductId.GetValueOrDefault(),
                    Quantity = request.Quantity,
                    Value = request.Value
                });

            var request = new AddStockRequest
            {
                ProductId = 1,
                Quantity = 2,
                Value = "val"
            };

            var res = await _service.AddStockAsync(request);

            Assert.False(res.Success);
            Assert.Equal(ErrorMessage, res.Error);
        }

        [Fact]
        public async Task DeleteStockByIdAsync_Success()
        {
            var stocks = new List<Stock>
            {
                new Stock { Id = 1 },
                new Stock { Id = 2 },
                new Stock { Id = 3 },
                new Stock { Id = 4 },
            };

            _stockRepository.Setup(x => x.DeleteStockByIdAsync(It.IsAny<int>()))
                .Callback((int id) => stocks.Remove(stocks.Find(x => x.Id == id)));

            const int Id = 3;

            var res = await _service.DeleteStockByIdAsync(Id);

            Assert.True(res.Success);
            Assert.DoesNotContain(stocks, x => x.Id == Id);
        }

        [Fact]
        public async Task DeleteStockByIdAsync_ErrorThrown_Fail()
        {
            const string ErrorMessage = "Error";
            _stockRepository.Setup(x => x.DeleteStockByIdAsync(It.IsAny<int>()))
                .Callback((int id) => throw new Exception(ErrorMessage));

            const int Id = 3;

            var res = await _service.DeleteStockByIdAsync(Id);

            Assert.False(res.Success);
            Assert.Equal(ErrorMessage, res.Error);
        }

        [Fact]
        public async Task UpdateStockAsync_Success()
        {
            var stock = new Stock
            {
                Id = 1,
                Value = "val",
                Quantity = 10
            };

            _stockRepository.Setup(x => x.UpdateStockAsync(It.IsAny<Stock>()));

            _stockRepository.Setup(x => x.GetStockById(It.IsAny<int>(), It.IsAny<Func<Stock, Stock>>()))
                .Returns(stock);

            var request = new UpdateStockRequest
            {
                Id = 1,
                Quantity = 20,
                Value = "new val"
            };

            var res = await _service.UpdateStockAsync(request);

            Assert.True(res.Success);
            Assert.Equal(request.Value, stock.Value);
            Assert.Equal(request.Quantity, stock.Quantity);
        }

        [Fact]
        public async Task UpdateStockAsync_NullRequest_Success()
        {
            var stock = new Stock
            {
                Id = 1,
                Value = "val",
                Quantity = 10
            };

            _stockRepository.Setup(x => x.UpdateStockAsync(It.IsAny<Stock>()));

            _stockRepository.Setup(x => x.GetStockById(It.IsAny<int>(), It.IsAny<Func<Stock, Stock>>()))
                .Returns(stock);

            var request = new UpdateStockRequest
            {
                Id = 1,
                Quantity = null,
                Value = null
            };

            var res = await _service.UpdateStockAsync(request);

            Assert.True(res.Success);
            Assert.NotEqual(request.Value, stock.Value);
            Assert.NotEqual(request.Quantity, stock.Quantity);
        }

        [Fact]
        public async Task UpdateStockAsync_InvalidRequest_Fail()
        {
            var stock = new Stock
            {
                Id = 1,
                Value = "val",
                Quantity = 10
            };

            _stockRepository.Setup(x => x.UpdateStockAsync(It.IsAny<Stock>()));

            _stockRepository.Setup(x => x.GetStockById(It.IsAny<int>(), It.IsAny<Func<Stock, Stock>>()))
                .Returns(stock);

            var request = new UpdateStockRequest
            {
                Id = 1,
                Quantity = 20,
                Value = new string('a', 50)
            };

            var res = await _service.UpdateStockAsync(request);

            Assert.False(res.Success);
            Assert.NotEqual(request.Value, stock.Value);
            Assert.NotEqual(request.Quantity, stock.Quantity);
        }

        [Fact]
        public async Task UpdateStockAsync_ExceptionThrown_Fail()
        {
            var stock = new Stock
            {
                Id = 1,
                Value = "val",
                Quantity = 10
            };

            const string Error = "error";

            _stockRepository.Setup(x => x.UpdateStockAsync(It.IsAny<Stock>()))
                .Callback(() => throw new Exception(Error));

            _stockRepository.Setup(x => x.GetStockById(It.IsAny<int>(), It.IsAny<Func<Stock, Stock>>()))
                .Returns(stock);

            var request = new UpdateStockRequest
            {
                Id = 1,
                Quantity = 20,
                Value = "new value"
            };

            var res = await _service.UpdateStockAsync(request);

            Assert.False(res.Success);
            Assert.Equal(Error, res.Error);
        }
    }
}
