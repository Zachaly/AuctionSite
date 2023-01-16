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
            _dbContext.UserInfo.Add(userInfo);

            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteUserInfoAsync(string id)
        {
            _dbContext.UserInfo.Remove(_dbContext.UserInfo.Find(id));

            return _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetUserInfoByIdAsync<T>(string id, Func<UserInfo, T> selector)
            => _dbContext.UserInfo
                .Where(info => info.Id == id)
                .Select(selector)
                .FirstOrDefault();

        public Task UpdateUserInfoAsync(UserInfo userInfo)
        {
            _dbContext.UserInfo.Update(userInfo);

            return _dbContext.SaveChangesAsync();
        }
    }
}
