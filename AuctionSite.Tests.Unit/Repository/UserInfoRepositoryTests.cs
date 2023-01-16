using AuctionSite.Database.Repository;
using AuctionSite.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSite.Tests.Unit.Repository
{
    public class UserInfoRepositoryTests : DatabaseTest
    {
        [Fact]
        public async Task AddUserInfoAsync()
        {
            var repository = new UserInfoRepository(_dbContext);

            var info = new UserInfo
            {
                Id = "id",
                FirstName = "name",
            };

            await repository.AddUserInfoAsync(info);

            Assert.Contains(info, _dbContext.UserInfo);
        }

        [Fact]
        public async Task DeleteUserInfoAsync()
        {
            var infos = new List<UserInfo>
            {
                new UserInfo { Id = "id1" },
                new UserInfo { Id = "id2" },
                new UserInfo { Id = "id3" },
                new UserInfo { Id = "id4" },
            };

            AddContent(infos);

            var repository = new UserInfoRepository(_dbContext);

            const string Id = "id3";

            await repository.DeleteUserInfoAsync(Id);

            Assert.DoesNotContain(infos.FirstOrDefault(x => x.Id == Id), _dbContext.UserInfo);
        }

        [Fact]
        public async Task GetUserInfoAsync()
        {
            var infos = new List<UserInfo>
            {
                new UserInfo { Id = "id1", FirstName = "fname1" },
                new UserInfo { Id = "id2", FirstName = "fname2" },
                new UserInfo { Id = "id3", FirstName = "fname3" },
                new UserInfo { Id = "id4", FirstName = "fname4" },
            };

            AddContent(infos);

            var repository = new UserInfoRepository(_dbContext);

            const string Id = "id2";

            var info = await repository.GetUserInfoByIdAsync(Id, x => x);

            Assert.Equal(Id, info.Id);
            Assert.Equal(infos.FirstOrDefault(x => x.Id == Id).FirstName, info.FirstName);
        }

        [Fact]
        public async Task UpdateUserInfoAsync()
        {
            var infos = new List<UserInfo>
            {
                new UserInfo { Id = "id1", FirstName = "fname1" },
                new UserInfo { Id = "id2", FirstName = "fname2" },
                new UserInfo { Id = "id3", FirstName = "fname3" },
                new UserInfo { Id = "id4", FirstName = "fname4" },
            };

            AddContent(infos);

            var repository = new UserInfoRepository(_dbContext);

            const string Id = "id2";
            const string NewName = "new fname";

            var info = _dbContext.UserInfo.Find(Id);

            info.FirstName = NewName;

            await repository.UpdateUserInfoAsync(info);

            info = _dbContext.UserInfo.Find(Id);

            Assert.Equal(NewName, info.FirstName);
        }
    }
}
