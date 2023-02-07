using AuctionSite.Database.Repository;
using AuctionSite.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSite.Tests.Unit.Repository
{
    public class ListRepositoryTests : DatabaseTest
    {
        private readonly ListRepository _repository;

        public ListRepositoryTests() : base()
        {
            _repository = new ListRepository(_dbContext);
        }

        [Fact]
        public async Task AddListAsync()
        {
            var list = new SaveList
            {
                UserId = "id",
                Name = "name",
            };

            await _repository.AddListAsync(list);

            Assert.Contains(_dbContext.SaveList, x => x.Name == list.Name);
        }

        [Fact]
        public async Task DeleteListByIdAsync()
        {
            const int Id = 3;

            AddContent(new List<SaveList>
            {
                new SaveList { Id = 1, UserId = "id", Name = "name" },
                new SaveList { Id = 2, UserId = "id", Name = "name" },
                new SaveList { Id = Id, UserId = "id", Name = "name" },
                new SaveList { Id = 4, UserId = "id", Name = "name" },
            });

            await _repository.DeleteListByIdAsync(Id);

            Assert.DoesNotContain(_dbContext.SaveList, x => x.Id == Id);
        }

        [Fact]
        public async Task GetListsByUserId()
        {
            var lists = new List<SaveList>
            {
                new SaveList { Id = 1, UserId = "id2", Name = "name" },
                new SaveList { Id = 2, UserId = "id", Name = "name" },
                new SaveList { Id = 3, UserId = "id2", Name = "name" },
                new SaveList { Id = 4, UserId = "id3", Name = "name" },
                new SaveList { Id = 5, UserId = "id", Name = "name" },
                new SaveList { Id = 6, UserId = "id1", Name = "name" },
                new SaveList { Id = 7, UserId = "id", Name = "name" },
            };

            AddContent(lists);

            const string Id = "id";

            var res = _repository.GetUserLists(Id, x => x.Id);

            Assert.Equivalent(lists.Where(x => x.UserId == Id).Select(x => x.Id), res);
        }

        [Fact]
        public void GetListById()
        {
            var list = new SaveList
            {
                Id = 3,
                Name = "name",
                UserId = "id",
                Stocks = new List<ListStock>()
            };

            AddContent(new List<SaveList>
            {
                new SaveList { Id = 1, UserId = "id", Name = "name1" },
                new SaveList { Id = 2, UserId = "id", Name = "name2" },
                list,
                new SaveList { Id = 4, UserId = "id", Name = "name3" },
            });

            var res = _repository.GetListById(list.Id, x => x);

            Assert.Equal(list.Id, res.Id);
            Assert.Equal(list.Name, res.Name);
            Assert.NotNull(res.Stocks);
        }
    }
}
