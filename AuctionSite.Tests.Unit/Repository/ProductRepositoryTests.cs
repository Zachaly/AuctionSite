using AuctionSite.Database.Repository;
using AuctionSite.Domain.Entity;

namespace AuctionSite.Tests.Unit.Repository
{
    public class ProductRepositoryTests : DatabaseTest
    {
        private readonly ProductRepository _repository;

        public ProductRepositoryTests() : base() 
        {
            _repository = new ProductRepository(_dbContext);
        }

        [Fact]
        public async Task AddProductAsync()
        {
            var product = new Product
            {
                Description = "desc",
                Name = "name",
                OptionName = "optname",
                OwnerId = "id",
                Price = 123,
                Options = new List<ProductOption>
                {
                    new ProductOption { Quantity = 2, Value = "val" }
                },
            };

            await _repository.AddProductAsync(product);

            Assert.Contains(_dbContext.Product, x => x.Description == product.Description);
            Assert.Contains(_dbContext.ProductOption, x => x.Quantity == product.Options.First().Quantity);
        }

        [Fact]
        public async Task DeleteProductByIdAsync()
        {
            AddContent(new List<Product> 
            {
                new Product { Id = 1, Description = "desc", Name = "name", OptionName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 2, Description = "desc", Name = "name", OptionName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 3, Description = "desc", Name = "name", OptionName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 4, Description = "desc", Name = "name", OptionName = "optname", OwnerId = "id", Price = 123 },
            });

            const int Id = 3;
            
            await _repository.DeleteProductByIdAsync(Id);

            Assert.DoesNotContain(_dbContext.Product, x => x.Id == Id);
        }

        [Fact]
        public void GetProducts()
        {
            AddContent(new List<Product>
            {
                new Product { Id = 1, Description = "desc", Name = "name", OptionName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 2, Description = "desc", Name = "name", OptionName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 3, Description = "desc", Name = "name", OptionName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 4, Description = "desc", Name = "name", OptionName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 5, Description = "desc", Name = "name", OptionName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 6, Description = "desc", Name = "name", OptionName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 7, Description = "desc", Name = "name", OptionName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 8, Description = "desc", Name = "name", OptionName = "optname", OwnerId = "id", Price = 123 },
            });

            var res = _repository.GetProducts(1, 4, x => x);

            Assert.Equivalent(_dbContext.Product.Skip(4).Take(4).Select(x => x.Id), res.Select(x => x.Id));
        }

        [Fact]
        public async Task GetProductById()
        {
            AddContent(new List<Product>
            {
                new Product { Id = 1, Description = "desc", Name = "name", OptionName = "optname", OwnerId = "id", Price = 123 },
                new Product 
                { 
                    Id = 2,
                    Description = "desc test",
                    Name = "name test",
                    OptionName = "optname test",
                    OwnerId = "id", 
                    Price = 123,
                    Options = new List<ProductOption>
                    {
                        new ProductOption { Quantity = 5, Value = "val" }
                    },
                    Owner = new ApplicationUser { Id = "id", UserName = "usrname" }
                },
                new Product { Id = 3, Description = "desc", Name = "name", OptionName = "optname", OwnerId = "id", Price = 123 },
            });

            const int Id = 2;
            var res = _repository.GetProductById(Id, x => x);

            Assert.Equal(_dbContext.Product.Find(Id).Name, res.Name);
            Assert.NotEmpty(res.Options);
            Assert.NotNull(res.Owner);
            Assert.NotNull(res.Images);
        }
    }
}
