using AuctionSite.Application;
using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.ListStock;
using AuctionSite.Models.SaveList.Request;
using Moq;

namespace AuctionSite.Tests.Unit.Factory
{
    public class ListFactoryTests
    {
        private readonly ListFactory _factory;

        public ListFactoryTests()
        {
            var stockFactory = new Mock<IListStockFactory>();

            stockFactory.Setup(x => x.CreateModel(It.IsAny<ListStock>()))
                .Returns(new ListStockModel());

            _factory = new ListFactory(stockFactory.Object);
        }

        [Fact]
        public void Create()
        {
            var request = new AddListRequest
            {
                UserId = "id",
                Name = "name"
            };

            var list = _factory.Create(request);

            Assert.Equal(request.UserId, list.UserId);
            Assert.Equal(request.Name, list.Name);
        }

        [Fact]
        public void CreateListItem()
        {
            var list = new SaveList { Id = 1, Name = "name" };

            var item = _factory.CreateListItem(list);

            Assert.Equal(list.Id, item.Id);
            Assert.Equal(list.Name, item.Name);
        }

        [Fact]
        public void CreateModel()
        {
            var list = new SaveList { Id = 1, Name = "name", Stocks = new List<ListStock> { new ListStock(), new ListStock() } };
            
            var model = _factory.CreateModel(list);

            Assert.Equal(list.Id, model.Id);
            Assert.Equal(list.Name, model.Name);
            Assert.Equal(list.Stocks.Count, model.Items.Count());
        }
    }
}
