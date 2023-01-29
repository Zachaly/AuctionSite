using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace AuctionSite.Application
{
    [Implementation(typeof(IFileService))]
    public class FileService : IFileService
    {
        private readonly string _profilePicturePath;
        private readonly string _productPicturePath;
        private readonly string _defaultPicture;

        public FileService(IConfiguration configuration)
        {
            _profilePicturePath = configuration["Image:ProfilePath"];
            _productPicturePath = configuration["Image:ProductPath"];
            _defaultPicture = configuration["Image:Default"];
        }

        public FileStream GetProductPicture(string fileName)
        {
            throw new NotImplementedException();
        }

        public FileStream GetProfilePicture(string fileName)
        {
            var name = string.IsNullOrEmpty(fileName) ? _defaultPicture : fileName;

            var path = Path.Combine(_profilePicturePath, name);
            
            return File.OpenRead(path);
        }

        public void RemoveProfilePicture(string fileName)
        {
            if(fileName == _defaultPicture || string.IsNullOrEmpty(fileName))
            {
                return;
            }

            var path = Path.Combine(_profilePicturePath, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public Task<IEnumerable<string>> SaveProductImages(IEnumerable<IFormFile> files)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveProfilePicture(IFormFile file)
        {
            if(file is null)
            {
                return _defaultPicture;
            }

            try
            {
                Directory.CreateDirectory(_profilePicturePath);

                var fileName = $"{Guid.NewGuid()}.png";

                using (var stream = File.Create(Path.Combine(_profilePicturePath, fileName)))
                {
                    await file.CopyToAsync(stream);
                }

                return fileName;
            }
            catch (Exception ex)
            {
                return _defaultPicture;
            }
        }

    }
}
