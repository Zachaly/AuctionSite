using Microsoft.AspNetCore.Http;

namespace AuctionSite.Application.Abstraction
{
    public interface IFileService
    {
        FileStream GetProfilePicture(string fileName);
        void RemoveProfilePicture(string fileName);
        Task<string> SaveProfilePicture(IFormFile file);
    }
}
