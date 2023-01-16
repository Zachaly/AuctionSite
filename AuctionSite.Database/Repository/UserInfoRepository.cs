using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;

namespace AuctionSite.Database.Repository
{
    [Implementation(typeof(IUserInfoRepository))]
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly AppDbContext _dbContext;

        public UserInfoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddUserInfoAsync(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserInfoAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetUserInfoByIdAsync<T>(string id, Func<UserInfo, T> selector)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserInfoAsync(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }
    }
}
