﻿using AuctionSite.Application;
using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Models;
using AuctionSite.Models.Product;
using AuctionSite.Models.Product.Request;
using AuctionSite.Models.Stock.Request;
using AuctionSite.Models.Response;
using Moq;

namespace AuctionSite.Tests.Unit.Service
{
    public class ProductServiceTests : ServiceTest
    {
        private readonly Mock<IProductFactory> _productFactory;
        private readonly Mock<IProductRepository> _productRepository;
        private readonly ProductService _service;

        public ProductServiceTests() : base()
        {
            _productFactory = new Mock<IProductFactory>();
            _productRepository = new Mock<IProductRepository>();

            _service = new ProductService(_responseFactory.Object, _productFactory.Object, _productRepository.Object);
        }

        [Fact]
        public async Task AddProductAsync_Success()
        {
            var products = new List<Product>();

            _productRepository.Setup(x => x.AddProductAsync(It.IsAny<Product>()))
                .Callback((Product product) => products.Add(product));

            _productFactory.Setup(x => x.Create(It.IsAny<AddProductRequest>()))
                .Returns((AddProductRequest request) => new Product
                {
                    Name = request.Name
                });

            _responseFactory.Setup(x => x.CreateSuccessWithCreatedId(It.IsAny<int>()))
                .Returns((int id) => new ResponseModel { Success = true, NewEntityId = id });

            var request = new AddProductRequest
            {
                Description = "description",
                Name = "name",
                StockName = "name",
                UserId = "id",
                Price = 123,
                Stocks = new List<AddStockRequest> { new AddStockRequest { Quantity = 1, Value = "val" } }
            };

            var res = await _service.AddProductAsync(request);

            Assert.True(res.Success);
            Assert.Contains(products, x => x.Name == request.Name);
        }

        [Fact]
        public async Task AddProductAsync_ExceptionThrown_Fail()
        {
            var products = new List<Product>();

            const string ErrorMessage = "error";

            _productRepository.Setup(x => x.AddProductAsync(It.IsAny<Product>()))
                .Callback((Product product) => throw new Exception(ErrorMessage));

            _productFactory.Setup(x => x.Create(It.IsAny<AddProductRequest>()))
                .Returns((AddProductRequest request) => new Product
                {
                    Name = request.Name
                });

            var request = new AddProductRequest
            {
                Description = "description",
                Name = "name",
                StockName = "name",
                UserId = "id",
                Price = 123,
                Stocks = new List<AddStockRequest> { new AddStockRequest { Value = "val", Quantity = 1 } }
            };

            var res = await _service.AddProductAsync(request);

            Assert.False(res.Success);
            Assert.Equal(ErrorMessage, res.Error);
            Assert.DoesNotContain(products, x => x.Name == request.Name);
        }

        [Fact]
        public async Task DeleteProductByIdAsync_Success()
        {
            var products = new List<Product>
            {
                new Product { Id = 1 },
                new Product { Id = 2 },
                new Product { Id = 3 },
                new Product { Id = 4 },
            };

            _productRepository.Setup(x => x.DeleteProductByIdAsync(It.IsAny<int>()))
                .Callback((int id) => products.Remove(products.Find(x => x.Id == id)));

            const int Id = 3;

            var res = await _service.DeleteProductByIdAsync(Id);

            Assert.True(res.Success);
            Assert.DoesNotContain(products, x => x.Id == Id);
        }

        [Fact]
        public async Task DeleteProductByIdAsync_ExceptionThrown_Fail()
        {
            var products = new List<Product>
            {
                new Product { Id = 1 },
                new Product { Id = 2 },
                new Product { Id = 3 },
                new Product { Id = 4 },
            };

            const string ErrorMessage = "error";

            _productRepository.Setup(x => x.DeleteProductByIdAsync(It.IsAny<int>()))
                .Callback((int id) => throw new Exception(ErrorMessage));

            const int Id = 3;

            var res = await _service.DeleteProductByIdAsync(Id);

            Assert.False(res.Success);
            Assert.Contains(products, x => x.Id == Id);
            Assert.Equal(ErrorMessage, res.Error);
        }

        [Fact]
        public void GetProductById_Success()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Description = "Test desc",
            };

            _productRepository.Setup(x => x.GetProductById(It.IsAny<int>(), It.IsAny<Func<Product, ProductModel>>()))
                .Returns((int id, Func<Product, ProductModel> selector) => selector(product));

            _productFactory.Setup(x => x.CreateModel(It.IsAny<Product>()))
                .Returns((Product product) => new ProductModel { Id = product.Id, Name = product.Name, Description = product.Description });

            MockDataResponse<ProductModel>();

            var res = _service.GetProductById(product.Id);

            Assert.True(res.Success);
            Assert.Equal(product.Name, res.Data.Name);
            Assert.Equal(product.Description, res.Data.Description);
        }

        [Fact]
        public void GetProductById_ProductNotFound_Fail()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Description = "Test desc",
            };

            _productRepository.Setup(x => x.GetProductById(It.IsAny<int>(), It.IsAny<Func<Product, ProductModel>>()))
                .Returns((int id, Func<Product, ProductModel> selector) => null);

            _productFactory.Setup(x => x.CreateModel(It.IsAny<Product>()))
                .Returns((Product product) => new ProductModel { Id = product.Id, Name = product.Name, Description = product.Description });

            MockDataResponse<ProductModel>();

            var res = _service.GetProductById(product.Id);

            Assert.False(res.Success);
            Assert.Null(res.Data);
        }

        [Fact]
        public void GetProductById_ExceptionThrown_Fail()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Description = "Test desc",
            };

            const string ErrorMessage = "error";

            _productRepository.Setup(x => x.GetProductById(It.IsAny<int>(), It.IsAny<Func<Product, ProductModel>>()))
                .Returns((int id, Func<Product, ProductModel> selector) => throw new Exception(ErrorMessage));

            _productFactory.Setup(x => x.CreateModel(It.IsAny<Product>()))
                .Returns((Product product) => new ProductModel { Id = product.Id, Name = product.Name, Description = product.Description });

            MockDataResponse<ProductModel>();

            var res = _service.GetProductById(product.Id);

            Assert.False(res.Success);
            Assert.Null(res.Data);
            Assert.Equal(ErrorMessage, res.Error);
        }

        [Theory]
        [InlineData(0, 5)]
        [InlineData(5, 1)]
        [InlineData(null, 2)]
        [InlineData(0, null)]
        public void GetProducts_Success(int? pageIndex, int? pageSize)
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "name1" },
                new Product { Id = 2, Name = "name2" },
                new Product { Id = 3, Name = "name3" },
                new Product { Id = 4, Name = "name4" },
                new Product { Id = 5, Name = "name5" },
                new Product { Id = 6, Name = "name6" },
                new Product { Id = 7, Name = "name7" },
                new Product { Id = 8, Name = "name8" },
                new Product { Id = 9, Name = "name9" },
                new Product { Id = 10, Name = "name10" },
                new Product { Id = 11, Name = "name11" },
            };

            _productRepository.Setup(x => x.GetProducts(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Func<Product, ProductListItemModel>>()))
                .Returns((int index, int pageSize, Func<Product, ProductListItemModel> selector)
                    => products.Skip(index * pageSize).Take(pageSize).Select(selector));

            _productFactory.Setup(x => x.CreateListItem(It.IsAny<Product>()))
                .Returns((Product prod) => new ProductListItemModel { Id = prod.Id, Name = prod.Name });

            MockDataResponse<IEnumerable<ProductListItemModel>>();

            var request = new PagedRequest
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
            };

            var res = _service.GetProducts(request);

            var testList = products.Skip((pageIndex ?? 0) * (pageSize ?? 10)).Take(pageSize ?? 10).ToList();

            Assert.True(res.Success);
            Assert.Equal(testList.Count, res.Data.Count());
            Assert.Equivalent(testList.Select(x => x.Id), res.Data.Select(x => x.Id), true);
        }

        [Fact]
        public void GetProducts_ExceptionThrown_Fail()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "name1" },
                new Product { Id = 2, Name = "name2" },
                new Product { Id = 3, Name = "name3" },
                new Product { Id = 4, Name = "name4" },
                new Product { Id = 5, Name = "name5" },
                new Product { Id = 6, Name = "name6" },
                new Product { Id = 7, Name = "name7" },
                new Product { Id = 8, Name = "name8" },
                new Product { Id = 9, Name = "name9" },
                new Product { Id = 10, Name = "name10" },
                new Product { Id = 11, Name = "name11" },
            };

            const string ErrorMessage = "error";

            _productRepository.Setup(x => x.GetProducts(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Func<Product, ProductListItemModel>>()))
                .Returns((int index, int pageSize, Func<Product, ProductListItemModel> selector)
                    => throw new Exception(ErrorMessage));

            _productFactory.Setup(x => x.CreateListItem(It.IsAny<Product>()))
                .Returns((Product prod) => new ProductListItemModel { Id = prod.Id, Name = prod.Name });

            MockDataResponse<IEnumerable<ProductListItemModel>>();

            var request = new PagedRequest
            {
                PageIndex = 1,
                PageSize = 2,
            };

            var res = _service.GetProducts(request);

            Assert.False(res.Success);
            Assert.Null(res.Data);
        }

        [Fact]
        public void GetPageCount_RequestWithSize()
        {
            MockDataResponse<int>();

            const int Count = 20;

            _productRepository.Setup(x => x.GetPageCount(It.IsAny<int>()))
                .Returns(Count);

            var request = new GetPageCountRequest { PageSize = 2 };

            var res = _service.GetPageCount(request);

            Assert.True(res.Success);
            Assert.Equal(Count, res.Data);
        }

        [Fact]
        public void GetPageCount_RequestWithoutSize()
        {
            MockDataResponse<int>();

            _productRepository.Setup(x => x.GetPageCount(It.IsAny<int>()))
                .Returns((int size) => size);

            var request = new GetPageCountRequest { };

            var res = _service.GetPageCount(request);

            Assert.True(res.Success);
            Assert.Equal(10, res.Data);
        }
    }
}
