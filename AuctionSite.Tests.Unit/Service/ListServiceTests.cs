using AuctionSite.Application;
using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.SaveList;
using AuctionSite.Models.SaveList.Request;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSite.Tests.Unit.Service
{
    public class ListServiceTests : ServiceTest
    {
        private readonly Mock<IListFactory> _listFactory;
        private readonly Mock<IListRepository> _listRepository;
        private readonly ListService _service;

        public ListServiceTests() : base()
        {
            _listFactory = new Mock<IListFactory>();
            _listRepository = new Mock<IListRepository>();

            _service = new ListService(_listRepository.Object, _listFactory.Object, _responseFactory.Object);
        }

        [Fact]
        public async Task AddListAsync_Success()
        {
            var lists = new List<SaveList>();

            _listRepository.Setup(x => x.AddListAsync(It.IsAny<SaveList>()))
                .Callback((SaveList list) => lists.Add(list));

            _listFactory.Setup(x => x.Create(It.IsAny<AddListRequest>()))
                .Returns((AddListRequest request) => new SaveList { Name = request.Name });

            var request = new AddListRequest
            {
                Name = "name"
            };

            var res = await _service.AddListAsync(request);

            Assert.True(res.Success);
            Assert.Contains(lists, x => x.Name == request.Name);
        }

        [Fact]
        public async Task AddListAsync_ExceptionThrown_Fail()
        {
            var lists = new List<SaveList>();

            const string Error = "error";

            _listRepository.Setup(x => x.AddListAsync(It.IsAny<SaveList>()))
                .Callback((SaveList list) => throw new Exception(Error));

            _listFactory.Setup(x => x.Create(It.IsAny<AddListRequest>()))
                .Returns((AddListRequest request) => new SaveList { Name = request.Name });

            var request = new AddListRequest
            {
                Name = "name"
            };

            var res = await _service.AddListAsync(request);

            Assert.False(res.Success);
            Assert.Equal(Error, res.Error);
        }

        [Fact]
        public async Task DeleteListByIdAsync_Success()
        {
            var lists = new List<SaveList>
            {
                new SaveList { Id = 1 },
                new SaveList { Id = 2 },
                new SaveList { Id = 3 },
            };

            _listRepository.Setup(x => x.DeleteListByIdAsync(It.IsAny<int>()))
                .Callback((int id) => lists.Remove(lists.Find(x => x.Id == id)));

            const int Id = 3;

            var res = await _service.DeleteListByIdAsync(Id);

            Assert.True(res.Success);
            Assert.DoesNotContain(lists, x => x.Id == Id);
        }

        [Fact]
        public async Task DeleteListByIdAsync_ExceptionThrown_Fail()
        {
            var lists = new List<SaveList>
            {
                new SaveList { Id = 1 },
                new SaveList { Id = 2 },
                new SaveList { Id = 3 },
            };

            const string Error = "error";

            _listRepository.Setup(x => x.DeleteListByIdAsync(It.IsAny<int>()))
                .Callback((int id) => throw new Exception(Error));

            const int Id = 3;

            var res = await _service.DeleteListByIdAsync(Id);

            Assert.False(res.Success);
            Assert.Equal(Error, res.Error);
        }

        [Fact]
        public void GetListById_Success()
        {
            var list = new SaveList { Id = 1, Name = "name" };

            _listRepository.Setup(x => x.GetListById(It.IsAny<int>(), It.IsAny<Func<SaveList, ListModel>>()))
                .Returns((int _, Func<SaveList, ListModel> selector) => selector(list));

            _listFactory.Setup(x => x.CreateModel(It.IsAny<SaveList>()))
                .Returns((SaveList save) => new ListModel { Id = save.Id, Name = save.Name });

            MockDataResponse<ListModel>();

            var res = _service.GetListById(0);

            Assert.True(res.Success);
            Assert.Equal(list.Name, res.Data.Name);
            Assert.Equal(list.Id, res.Data.Id);
        }

        [Fact]
        public void GetListById_ExceptionThrown_Fail()
        {
            var list = new SaveList { Id = 1, Name = "name" };

            const string Error = "error";

            _listRepository.Setup(x => x.GetListById(It.IsAny<int>(), It.IsAny<Func<SaveList, ListModel>>()))
                .Returns(() => throw new Exception(Error));

            _listFactory.Setup(x => x.CreateModel(It.IsAny<SaveList>()))
                .Returns((SaveList save) => new ListModel { Id = save.Id, Name = save.Name });

            MockDataResponse<ListModel>();

            var res = _service.GetListById(0);

            Assert.False(res.Success);
            Assert.Equal(Error, res.Error);
            Assert.Null(res.Data);
        }

        [Fact]
        public void GetUserLists_Success()
        {
            var lists = new List<SaveList>
            {
                new SaveList { Id = 1, Name = "name" },
                new SaveList { Id = 2, Name = "name" },
                new SaveList { Id = 3, Name = "name" },
            };

            _listRepository.Setup(x => x.GetUserLists(It.IsAny<string>(), It.IsAny<Func<SaveList, ListListModel>>()))
                .Returns((string _, Func<SaveList, ListListModel> selector) => lists.Select(selector));

            _listFactory.Setup(x => x.CreateListItem(It.IsAny<SaveList>()))
                .Returns((SaveList list) => new ListListModel { Id = list.Id, Name = list.Name });

            MockDataResponse<IEnumerable<ListListModel>>();

            var res = _service.GetUserLists("");

            Assert.True(res.Success);
            Assert.Equivalent(lists.Select(x => x.Name), res.Data.Select(x => x.Name));
        }
    }
}
