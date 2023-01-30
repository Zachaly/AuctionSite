using AuctionSite.Database.Repository;
using AuctionSite.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSite.Tests.Unit.Repository
{
    public class ProductImageRepositoryTests : DatabaseTest
    {
        private readonly ProductImageRepository _repository;

        public ProductImageRepositoryTests() : base()
        {
            _repository = new ProductImageRepository(_dbContext);
        }

        [Fact]
        public async Task AddProductImagesAsync()
        {
            var images = new List<ProductImage>()
            {
                new ProductImage { FileName = "name1", ProductId = 2 },
                new ProductImage { FileName = "name2", ProductId = 2 },
                new ProductImage { FileName = "name3", ProductId = 2 },
                new ProductImage { FileName = "name4", ProductId = 2 },
            };

            await _repository.AddProductImagesAsync(images);

            Assert.All(_dbContext.ProductImage, x => images.Any(y => y.FileName == x.FileName));
        }

        [Fact]
        public async Task GetProductImagesByProductId()
        {
            var images = new List<ProductImage>()
            {
                new ProductImage { FileName = "name1", ProductId = 2 },
                new ProductImage { FileName = "name2", ProductId = 2 },
                new ProductImage { FileName = "name3", ProductId = 2 },
                new ProductImage { FileName = "name4", ProductId = 2 },
                new ProductImage { FileName = "name1", ProductId = 3 },
                new ProductImage { FileName = "name2", ProductId = 3 },
                new ProductImage { FileName = "name3", ProductId = 3 },
                new ProductImage { FileName = "name4", ProductId = 3 },
            };

            AddContent(images);

            const int Id = 3;

            var res = _repository.GetProductImagesByProductId(Id, x => x);

            Assert.All(res, x => Assert.Equal(Id, x.ProductId) );
        }

        [Fact]
        public async Task GetProductImageById()
        {
            var images = new List<ProductImage>()
            {
                new ProductImage { Id = 1, FileName = "name1", ProductId = 2 },
                new ProductImage { Id = 2, FileName = "name2", ProductId = 2 },
                new ProductImage { Id = 3, FileName = "name3", ProductId = 2 },
                new ProductImage { Id = 4, FileName = "name4", ProductId = 2 },
                new ProductImage { Id = 5, FileName = "name1", ProductId = 3 },
                new ProductImage { Id = 6, FileName = "name2", ProductId = 3 },
                new ProductImage { Id = 7, FileName = "name3", ProductId = 3 },
                new ProductImage { Id = 8, FileName = "name4", ProductId = 3 },
            };

            AddContent(images);

            const int Id = 3;

            var res = await _repository.GetProductImageById(Id, x => x);

            Assert.Equal(images.FirstOrDefault(x => x.Id == Id).FileName, res.FileName);
        }
    }
}
