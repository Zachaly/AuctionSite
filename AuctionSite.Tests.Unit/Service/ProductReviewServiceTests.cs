using AuctionSite.Application;
using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.ProductReview;
using AuctionSite.Models.ProductReview.Request;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSite.Tests.Unit.Service
{
    public class ProductReviewServiceTests : ServiceTest
    {
        private readonly Mock<IProductReviewRepository> _productReviewRepository;
        private readonly Mock<IProductReviewFactory> _productReviewFactory;
        private readonly ProductReviewService _service;

        public ProductReviewServiceTests()
        {
            _productReviewRepository = new Mock<IProductReviewRepository>();
            _productReviewFactory = new Mock<IProductReviewFactory>();

            _service = new ProductReviewService(_productReviewRepository.Object, _productReviewFactory.Object, _responseFactory.Object);
        }

        [Fact]
        public async Task AddProductReviewAsync_Success()
        {
            var reviews = new List<ProductReview>();

            _productReviewFactory.Setup(x => x.Create(It.IsAny<AddProductReviewRequest>()))
                .Returns((AddProductReviewRequest request) => new ProductReview { Content = request.Content });

            _productReviewRepository.Setup(x 
                => x.GetProductReviewByProductAndUserId(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Func<ProductReview, ProductReview>>()))
                .Returns(() => null);

            _productReviewRepository.Setup(x => x.AddReviewAsync(It.IsAny<ProductReview>()))
                .Callback((ProductReview review) => reviews.Add(review));

            var request = new AddProductReviewRequest
            {
                UserId = "id",
                Content = "content",
            };

            var res = await _service.AddProductReviewAsync(request);

            Assert.True(res.Success);
            Assert.Contains(reviews, x => x.Content == request.Content);
        }

        [Fact]
        public async Task AddProductReviewAsync_ReviewExists_Fail()
        {
            _productReviewRepository.Setup(x
                => x.GetProductReviewByProductAndUserId(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Func<ProductReview, ProductReview>>()))
                .Returns(new ProductReview());

            var request = new AddProductReviewRequest
            {
            };

            var res = await _service.AddProductReviewAsync(request);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task AddProductReviewAsync_ExceptionThrown_Fail()
        {
            _productReviewFactory.Setup(x => x.Create(It.IsAny<AddProductReviewRequest>()))
                .Returns((AddProductReviewRequest request) => new ProductReview { Content = request.Content });

            _productReviewRepository.Setup(x
                => x.GetProductReviewByProductAndUserId(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Func<ProductReview, ProductReview>>()))
                .Returns(() => null);

            const string Error = "error";

            _productReviewRepository.Setup(x => x.AddReviewAsync(It.IsAny<ProductReview>()))
                .Callback(() => throw new Exception(Error));

            var request = new AddProductReviewRequest
            {
                UserId = "id",
                Content = "content",
            };

            var res = await _service.AddProductReviewAsync(request);

            Assert.False(res.Success);
            Assert.Equal(Error, res.Error);
        }

        [Fact]
        public async Task DeleteProductReviewByIdAsync_Success()
        {
            var reviews = new List<ProductReview>
            {
                new ProductReview { Id = 1 },
                new ProductReview { Id = 2 },
                new ProductReview { Id = 3 },
                new ProductReview { Id = 4 },
            };

            _productReviewRepository.Setup(x => x.DeleteReviewByIdAsync(It.IsAny<int>()))
                .Callback((int id) => reviews.Remove(reviews.First(x => x.Id == id)));

            const int Id = 3;

            var res = await _service.DeleteProductReviewAsync(Id);

            Assert.True(res.Success);
            Assert.DoesNotContain(reviews, x => x.Id == Id);
        }

        [Fact]
        public async Task DeleteProductReviewByIdAsync_ExceptionThrown_Fail()
        {
            const string Error = "error";

            _productReviewRepository.Setup(x => x.DeleteReviewByIdAsync(It.IsAny<int>()))
                .Callback(() => throw new Exception(Error));

            var res = await _service.DeleteProductReviewAsync(0);

            Assert.False(res.Success);
            Assert.Equal(Error, res.Error);
        }

        [Fact]
        public async Task GetProductReviews_Success()
        {
            var reviews = new List<ProductReview>
            {
                new ProductReview { Id = 1 },
                new ProductReview { Id = 2 },
                new ProductReview { Id = 3 },
                new ProductReview { Id = 4 },
            };

            _productReviewFactory.Setup(x => x.CreateModel(It.IsAny<ProductReview>()))
                .Returns((ProductReview review) => new ProductReviewListModel { Id = review.Id });

            _productReviewRepository.Setup(x => x.GetReviewsByProductId(It.IsAny<int>(), It.IsAny<Func<ProductReview, ProductReviewListModel>>()))
                .Returns((int _, Func<ProductReview, ProductReviewListModel> selector)
                    => reviews.Select(selector));

            MockDataResponse<IEnumerable<ProductReviewListModel>>();

            var res = await _service.GetProductReviewsAsync(0);

            Assert.True(res.Success);
            Assert.Equivalent(reviews.Select(x => x.Id), res.Data.Select(x => x.Id));
        }

        [Fact]
        public async Task UpdateProductReviewAsync_Success()
        {
            var review = new ProductReview
            {
                Id = 1,
                Content = "content",
                Score = 1
            };

            _productReviewRepository.Setup(x => x.GetReviewById(It.IsAny<int>(), It.IsAny<Func<ProductReview, ProductReview>>()))
                .Returns(review);

            _productReviewRepository.Setup(x => x.UpdateReviewAsync(It.IsAny<ProductReview>()));

            var request = new UpdateProductReviewRequest
            {
                Id = 1,
                Content = "new content",
                Score = 10
            };

            var res = await _service.UpdateProductReviewAsync(request);

            Assert.True(res.Success);
            Assert.Equal(request.Content, review.Content);
            Assert.Equal(request.Score, review.Score);
        }

        [Fact]
        public async Task UpdateProductReviewAsync_NullRequest_NoChange_Success()
        {
            var review = new ProductReview
            {
                Id = 1,
                Content = "content",
                Score = 1
            };

            _productReviewRepository.Setup(x => x.GetReviewById(It.IsAny<int>(), It.IsAny<Func<ProductReview, ProductReview>>()))
                .Returns(review);

            _productReviewRepository.Setup(x => x.UpdateReviewAsync(It.IsAny<ProductReview>()));

            var request = new UpdateProductReviewRequest
            {
                Id = 1,
                Content = null,
                Score = null
            };

            var res = await _service.UpdateProductReviewAsync(request);

            Assert.True(res.Success);
            Assert.NotEqual(request.Content, review.Content);
            Assert.NotEqual(request.Score, review.Score);
        }

        [Fact]
        public async Task UpdateProductReviewAsync_ExceptionThrown_Fail()
        {
            var review = new ProductReview
            {
                Id = 1,
                Content = "content",
                Score = 1
            };

            _productReviewRepository.Setup(x => x.GetReviewById(It.IsAny<int>(), It.IsAny<Func<ProductReview, ProductReview>>()))
                .Returns(review);

            const string Error = "Error";

            _productReviewRepository.Setup(x => x.UpdateReviewAsync(It.IsAny<ProductReview>()))
                .Callback(() => throw new Exception(Error));

            var request = new UpdateProductReviewRequest
            {
                Id = 1,
                Content = null,
                Score = null
            };

            var res = await _service.UpdateProductReviewAsync(request);

            Assert.False(res.Success);
        }
    }
}
