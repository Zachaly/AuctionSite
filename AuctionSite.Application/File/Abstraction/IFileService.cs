using Microsoft.AspNetCore.Http;

namespace AuctionSite.Application.Abstraction
{
    public interface IFileService
    {
        FileStream GetProfilePicture(string fileName);
        void RemoveProfilePicture(string fileName);
        Task<string> SaveProfilePicture(IFormFile file);
        FileStream GetProductPicture(string fileName);
        Task<IEnumerable<string>> SaveProductImages(IEnumerable<IFormFile> files);
        void RemoveProductImage(string fileName);
    }
}
