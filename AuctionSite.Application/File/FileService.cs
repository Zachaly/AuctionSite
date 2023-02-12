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

        private FileStream GetFile(string filePath, string fileName)
        {
            var name = string.IsNullOrEmpty(fileName) ? _defaultPicture : fileName;

            var path = Path.Combine(filePath, name);

            return File.OpenRead(path);
        }

        public FileStream GetProductPicture(string fileName)
            => GetFile(_productPicturePath, fileName);

        public FileStream GetProfilePicture(string fileName)
            => GetFile(_profilePicturePath, fileName);

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

        public async Task<IEnumerable<string>> SaveProductImages(IEnumerable<IFormFile> files)
        {
            if(files.Count() < 0)
            {
                return new string[] { _defaultPicture };
            }

            var names = new List<string>();
            foreach(var file in files)
            {
                Directory.CreateDirectory(_productPicturePath);
                var fileName = $"{Guid.NewGuid()}.png";

                using (var stream = File.Create(Path.Combine(_productPicturePath, fileName)))
                {
                    await file.CopyToAsync(stream);
                }

                names.Add(fileName);
            }

            return names;
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

        public void RemoveProductImage(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
