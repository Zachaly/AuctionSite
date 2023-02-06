using AuctionSite.Application;
using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.ListStock.Request;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSite.Tests.Unit.Service
{
    public class ListStockServiceTests : ServiceTest
    {
        private readonly Mock<IListStockRepository> _listStockRepository;
        private readonly Mock<IListStockFactory> _listStockFactory;
        private readonly ListStockService _service;

        public ListStockServiceTests() : base() 
        {
            _listStockRepository = new Mock<IListStockRepository>();
            _listStockFactory = new Mock<IListStockFactory>();

            _service = new ListStockService(_listStockRepository.Object, _listStockFactory.Object, _responseFactory.Object);
        }

        [Fact]
        public async Task AddListStockAsync_Success()
        {
            var stocks = new List<ListStock>();

            _listStockRepository.Setup(x => x.AddListStockAsync(It.IsAny<ListStock>()))
                .Callback((ListStock stock) => stocks.Add(stock));

            _listStockFactory.Setup(x => x.Create(It.IsAny<AddListStockRequest>()))
                .Returns((AddListStockRequest request) => new ListStock { ListId = request.ListId });

            var request = new AddListStockRequest { ListId = 1 };

            var res = await _service.AddListStockAsync(request);

            Assert.True(res.Success);
            Assert.Contains(stocks, x => x.ListId == request.ListId);
        }

        [Fact]
        public async Task AddListStockAsync_ExceptionThrown_Fail()
        {
            var stocks = new List<ListStock>();

            const string Error = "error";

            _listStockRepository.Setup(x => x.AddListStockAsync(It.IsAny<ListStock>()))
                .Callback((ListStock stock) => throw new Exception(Error));

            _listStockFactory.Setup(x => x.Create(It.IsAny<AddListStockRequest>()))
                .Returns((AddListStockRequest request) => new ListStock { ListId = request.ListId });

            var request = new AddListStockRequest { ListId = 1 };

            var res = await _service.AddListStockAsync(request);

            Assert.False(res.Success);
            Assert.Equal(Error, res.Error);
        }

        [Fact]
        public async Task DeleteListStockByIdAsync_Success()
        {
            var stocks = new List<ListStock>
            {
                new ListStock { Id = 1 },
                new ListStock { Id = 2 },
                new ListStock { Id = 3 },
                new ListStock { Id = 4 },
            };

            _listStockRepository.Setup(x => x.DeleteListByIdAsyncStock(It.IsAny<int>()))
                .Callback((int id) => stocks.Remove(stocks.Find(x => x.Id == id)));

            const int Id = 3;

            var res = await _service.DeleteListStockByIdAsync(Id);

            Assert.True(res.Success);
            Assert.DoesNotContain(stocks, x => x.Id == Id);
        }

        [Fact]
        public async Task DeleteListStockByIdAsync_ExceptionThrown_Fail()
        {
            var stocks = new List<ListStock>
            {
                new ListStock { Id = 1 },
                new ListStock { Id = 2 },
                new ListStock { Id = 3 },
                new ListStock { Id = 4 },
            };

            const string Error = "error";

            _listStockRepository.Setup(x => x.DeleteListByIdAsyncStock(It.IsAny<int>()))
                .Callback((int id) => throw new Exception(Error));

            const int Id = 3;

            var res = await _service.DeleteListStockByIdAsync(Id);

            Assert.False(res.Success);
            Assert.Equal(Error, res.Error);
        }
    }
}
