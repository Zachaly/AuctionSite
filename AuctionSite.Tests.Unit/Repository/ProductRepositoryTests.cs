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
                StockName = "optname",
                OwnerId = "id",
                Price = 123,
                Stocks = new List<Stock>
                {
                    new Stock { Quantity = 2, Value = "val" }
                },
            };

            await _repository.AddProductAsync(product);

            Assert.Contains(_dbContext.Product, x => x.Description == product.Description);
            Assert.Contains(_dbContext.Stock, x => x.Quantity == product.Stocks.First().Quantity);
        }

        [Fact]
        public async Task DeleteProductByIdAsync()
        {
            AddContent(new List<Product> 
            {
                new Product { Id = 1, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 2, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 3, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 4, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
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
                new Product { Id = 1, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 2, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 3, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 4, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 5, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 6, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 7, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 8, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
            });

            var res = _repository.GetProducts(1, 4, x => x);

            Assert.Equivalent(_dbContext.Product.Skip(4).Take(4).Select(x => x.Id), res.Select(x => x.Id));
        }

        [Fact]
        public void GetProductsByUserId()
        {
            AddContent(new List<Product>
            {
                new Product { Id = 1, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id2", Price = 123 },
                new Product { Id = 2, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 3, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id1", Price = 123 },
                new Product { Id = 4, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 5, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id3", Price = 123 },
                new Product { Id = 6, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id4", Price = 123 },
                new Product { Id = 7, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id4", Price = 123 },
                new Product { Id = 8, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
            });

            const string Id = "id";

            var res = _repository.GetProductsByUserId(Id, 1, 2, x => x);

            Assert.Equivalent(_dbContext.Product.Where(x => x.OwnerId == Id).Skip(2).Take(2).Select(x => x.Id), res.Select(x => x.Id));
        }

        [Fact]
        public async Task GetProductById()
        {
            AddContent(new List<Product>
            {
                new Product { Id = 1, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product 
                { 
                    Id = 2,
                    Description = "desc test",
                    Name = "name test",
                    StockName = "optname test",
                    OwnerId = "id", 
                    Price = 123,
                    Stocks = new List<Stock>
                    {
                        new Stock { Quantity = 5, Value = "val" }
                    },
                    Owner = new ApplicationUser { Id = "id", UserName = "usrname" },
                    Category = new ProductCategory { Name = "cat" }
                },
                new Product { Id = 3, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
            });

            const int Id = 2;
            var res = _repository.GetProductById(Id, x => x);

            Assert.Equal(_dbContext.Product.Find(Id).Name, res.Name);
            Assert.NotEmpty(res.Stocks);
            Assert.NotNull(res.Owner);
            Assert.NotNull(res.Images);
            Assert.NotNull(res.Category);
        }

        [Fact]
        public void GetPageCount()
        {
            AddContent(new List<Product>
            {
                new Product { Id = 1, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 2, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 3, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 4, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 5, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 6, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 7, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 8, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 9, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
            });

            var count = _repository.GetPageCount(4);

            Assert.Equal(3, count);
        }

        [Fact]
        public void GetUserPageCount()
        {
            AddContent(new List<Product>
            {
                new Product { Id = 1, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id1", Price = 123 },
                new Product { Id = 2, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 3, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id2", Price = 123 },
                new Product { Id = 4, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 5, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id3", Price = 123 },
                new Product { Id = 6, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id4", Price = 123 },
                new Product { Id = 7, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id5", Price = 123 },
                new Product { Id = 8, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
                new Product { Id = 9, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123 },
            });

            var count = _repository.GetUserPageCount("id", 2);

            Assert.Equal(2, count);
        }

        [Fact]
        public async Task UpdateProductAsync()
        {
            var product = new Product
            {
                Id = 1,
                Description = "desc",
                Name = "name",
                StockName = "optname",
                OwnerId = "id",
                Price = 123,
            };

            AddContent(product);

            const int NewPrice = 321;

            product.Price = NewPrice;

            await _repository.UpdateProductAsync(product);

            Assert.Contains(_dbContext.Product, x => x.Id == product.Id && x.Price == NewPrice);
        }

        [Fact]
        public void GetProductsByCategoryId()
        {
            const int CategoryId = 2;

            AddContent(new List<Product>
            {
                new Product { Id = 1, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id2", Price = 123, CategoryId = 1 },
                new Product { Id = 2, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123, CategoryId = CategoryId },
                new Product { Id = 3, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id1", Price = 123, CategoryId = 1 },
                new Product { Id = 4, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123, CategoryId = 1 },
                new Product { Id = 5, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id3", Price = 123, CategoryId = CategoryId },
                new Product { Id = 6, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id4", Price = 123, CategoryId = 1 },
                new Product { Id = 7, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id4", Price = 123, CategoryId = CategoryId },
                new Product { Id = 8, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123, CategoryId = 1 },
            });

            var res = _repository.GetProductsByCategoryId(CategoryId, 0, 2, x => x);

            Assert.Equivalent(_dbContext.Product.Where(x => x.CategoryId == CategoryId).Take(2).Select(x => x.Id), res.Select(x => x.Id));
            Assert.All(res, x => Assert.Equal(CategoryId, x.CategoryId));
        }

        [Fact]
        public void GetCategoryPageCount()
        {
            AddContent(new List<Product>
            {
                new Product { Id = 1, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id1", Price = 123, CategoryId = 1 },
                new Product { Id = 2, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123, CategoryId = 3 },
                new Product { Id = 3, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id2", Price = 123, CategoryId = 3 },
                new Product { Id = 4, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123, CategoryId = 1 },
                new Product { Id = 5, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id3", Price = 123, CategoryId = 3 },
                new Product { Id = 6, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id4", Price = 123, CategoryId = 3 },
                new Product { Id = 7, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id5", Price = 123, CategoryId = 1 },
                new Product { Id = 8, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123, CategoryId = 3 },
                new Product { Id = 9, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123, CategoryId = 2 },
            });

            var count = _repository.GetCategoryPageCount(3, 3);

            Assert.Equal(2, count);
        }

        [Fact]
        public void SearchProducts_CategoryAndNameNull()
        {
            AddContent(new List<Product>
            {
                new Product { Id = 1, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id1", Price = 123, CategoryId = 1 },
                new Product { Id = 2, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123, CategoryId = 3 },
                new Product { Id = 3, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id2", Price = 123, CategoryId = 3 },
                new Product { Id = 4, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123, CategoryId = 1 },
                new Product { Id = 5, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id3", Price = 123, CategoryId = 3 },
                new Product { Id = 6, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id4", Price = 123, CategoryId = 3 },
                new Product { Id = 7, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id5", Price = 123, CategoryId = 1 },
                new Product { Id = 8, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123, CategoryId = 3 },
                new Product { Id = 9, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123, CategoryId = 2 },
            });

            var res = _repository.SearchProducts(null, null, 0, 3, x => x);

            Assert.Equal(_dbContext.Product.Take(3).Select(x => x.Id), res.Select(x => x.Id));
        }

        [Fact]
        public void SearchProducts_With_CategoryAndName()
        {
            AddContent(new List<Product>
            {
                new Product { Id = 1, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id1", Price = 123, CategoryId = 1 },
                new Product { Id = 2, Description = "desc", Name = "product", StockName = "optname", OwnerId = "id", Price = 123, CategoryId = 3 },
                new Product { Id = 3, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id2", Price = 123, CategoryId = 3 },
                new Product { Id = 4, Description = "desc", Name = "product", StockName = "optname", OwnerId = "id", Price = 123, CategoryId = 1 },
                new Product { Id = 5, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id3", Price = 123, CategoryId = 3 },
                new Product { Id = 6, Description = "desc", Name = "product", StockName = "optname", OwnerId = "id4", Price = 123, CategoryId = 3 },
                new Product { Id = 7, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id5", Price = 123, CategoryId = 1 },
                new Product { Id = 8, Description = "desc", Name = "product", StockName = "optname", OwnerId = "id", Price = 123, CategoryId = 3 },
                new Product { Id = 9, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123, CategoryId = 2 },
            });

            const int CategoryId = 2;
            const string Name = "name";

            var res = _repository.SearchProducts(CategoryId, Name, 0, 3, x => x);

            Assert.Equal(_dbContext.Product.Where(x => x.CategoryId == CategoryId && x.Name == Name).Take(3).Select(x => x.Id),
                res.Select(x => x.Id));
        }

        [Fact]
        public void GetPageCount_With_CategoryIdAndName()
        {
            AddContent(new List<Product>
            {
                new Product { Id = 1, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id1", Price = 123, CategoryId = 1 },
                new Product { Id = 2, Description = "desc", Name = "product", StockName = "optname", OwnerId = "id", Price = 123, CategoryId = 3 },
                new Product { Id = 3, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id2", Price = 123, CategoryId = 3 },
                new Product { Id = 4, Description = "desc", Name = "product", StockName = "optname", OwnerId = "id", Price = 123, CategoryId = 1 },
                new Product { Id = 5, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id3", Price = 123, CategoryId = 3 },
                new Product { Id = 6, Description = "desc", Name = "product", StockName = "optname", OwnerId = "id4", Price = 123, CategoryId = 3 },
                new Product { Id = 7, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id5", Price = 123, CategoryId = 1 },
                new Product { Id = 8, Description = "desc", Name = "product", StockName = "optname", OwnerId = "id", Price = 123, CategoryId = 3 },
                new Product { Id = 9, Description = "desc", Name = "name", StockName = "optname", OwnerId = "id", Price = 123, CategoryId = 2 },
            });

            const int CategoryId = 3;
            const string Name = "name";

            var res = _repository.GetPageCount(CategoryId, Name, 1);

            Assert.Equal(2, res);
        }
    }
}
