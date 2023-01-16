using AuctionSite.Domain.Entity;

namespace AuctionSite.Database.Repository.Abstraction
{
    public interface IUserInfoRepository
    {
        Task<T> GetUserInfoByIdAsync<T>(string id, Func<UserInfo, T> selector);
        Task AddUserInfoAsync(UserInfo userInfo);
        Task UpdateUserInfoAsync(UserInfo userInfo);
        Task DeleteUserInfoAsync(string id);
    }
}
