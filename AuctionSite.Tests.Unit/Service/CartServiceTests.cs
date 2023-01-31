using AuctionSite.Application;
using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.Cart;
using AuctionSite.Models.Cart.Request;
using AuctionSite.Models.Response;
using Moq;

namespace AuctionSite.Tests.Unit.Service
{
    public class CartServiceTests
    {
        private readonly Mock<IResponseFactory> _responseFactory;
        private readonly Mock<ICartFactory> _cartFactory;
        private readonly Mock<IStockOnHoldFactory> _stockOnHoldFactory;
        private readonly Mock<ICartRepository> _cartRepository;
        private readonly Mock<IStockOnHoldRepository> _stockOnHoldRepository;
        private readonly CartService _service;

        public CartServiceTests()
        {
            _responseFactory = new Mock<IResponseFactory>();
            _cartFactory = new Mock<ICartFactory>();
            _stockOnHoldFactory = new Mock<IStockOnHoldFactory>();
            _cartRepository = new Mock<ICartRepository>();
            _stockOnHoldRepository = new Mock<IStockOnHoldRepository>();

            _responseFactory.Setup(x => x.CreateSuccess())
                .Returns(new ResponseModel { Success = true });

            _responseFactory.Setup(x => x.CreateFailure(It.IsAny<string>()))
                .Returns((string msg) => new ResponseModel { Success = false, Error = msg });

            _service = new CartService(_cartRepository.Object, _cartFactory.Object, _stockOnHoldFactory.Object, _stockOnHoldRepository.Object, _responseFactory.Object);
        }

        [Fact]
        public async Task AddCart_Success()
        {
            var carts = new List<Cart>();

            _cartRepository.Setup(x => x.GetCartByUserId(It.IsAny<string>(), It.IsAny<Func<Cart, Cart>>()))
                .Returns(() => null);

            _cartRepository.Setup(x => x.AddCartAsync(It.IsAny<Cart>()))
                .Callback((Cart cart) => carts.Add(cart));

            _cartFactory.Setup(x => x.Create(It.IsAny<AddCartRequest>()))
                .Returns((AddCartRequest request) => new Cart { UserId = request.UserId });

            var request = new AddCartRequest { UserId = "id" };

            var res = await _service.AddCartAsync(request);

            Assert.True(res.Success);
            Assert.Contains(carts, x => x.UserId == request.UserId);
        }

        [Fact]
        public async Task AddCart_CartAlreadyExists_Success()
        {
            _cartRepository.Setup(x => x.GetCartByUserId(It.IsAny<string>(), It.IsAny<Func<Cart, Cart>>()))
                .Returns(() => new Cart());

            _responseFactory.Setup(x => x.CreateSuccess())
                .Returns(new ResponseModel { Success = true });

            var request = new AddCartRequest { UserId = "id" };

            var res = await _service.AddCartAsync(request);

            Assert.True(res.Success);
        }

        [Fact]
        public async Task AddCart_ExceptionThrown_Fail()
        {
            var carts = new List<Cart>();

            const string Error = "Error";

            _cartRepository.Setup(x => x.GetCartByUserId(It.IsAny<string>(), It.IsAny<Func<Cart, Cart>>()))
                .Returns(() => throw new Exception(Error));

            var request = new AddCartRequest { UserId = "id" };

            var res = await _service.AddCartAsync(request);

            Assert.False(res.Success);
            Assert.Equal(Error, res.Error);
            Assert.Contains(carts, x => x.UserId == request.UserId);
        }

        [Fact]
        public async Task AddToCartAsync_Success()
        {
            var cart = new Cart
            {
                Id = 1,
                StocksOnHold = new List<StockOnHold>(),
                UserId = "id"
            };

            _cartRepository.Setup(x => x.GetCartByUserId(It.IsAny<string>(), It.IsAny<Func<Cart, Cart>>()))
                .Returns(() => cart);

            _stockOnHoldRepository.Setup(x => x.AddStockOnHoldAsync(It.IsAny<StockOnHold>()))
                .Callback((StockOnHold stock) => cart.StocksOnHold.Add(stock));

            _stockOnHoldFactory.Setup(x => x.Create(It.IsAny<AddToCartRequest>(), It.IsAny<int>()))
                .Returns((AddToCartRequest request, int id) => new StockOnHold { CartId = id });

            var request = new AddToCartRequest
            {
                Quantity = 1,
                StockId = 2,
                UserId = "id"
            };

            var res = await _service.AddToCartAsync(request);

            Assert.True(res.Success);
            Assert.Contains(cart.StocksOnHold, x => x.StockId == request.StockId);
        }

        [Fact]
        public async Task AddToCartAsync_CartNotFound_Fail()
        {
            var cart = new Cart
            {
                Id = 1,
                StocksOnHold = new List<StockOnHold>(),
                UserId = "id"
            };

            _cartRepository.Setup(x => x.GetCartByUserId(It.IsAny<string>(), It.IsAny<Func<Cart, Cart>>()))
                .Returns(() => null);

            var request = new AddToCartRequest
            {
                Quantity = 1,
                StockId = 2,
                UserId = "id"
            };

            var res = await _service.AddToCartAsync(request);

            Assert.False(res.Success);
            Assert.DoesNotContain(cart.StocksOnHold, x => x.StockId == request.StockId);
        }

        [Fact]
        public async Task AddToCartAsync_ExceptionThrown_Fail()
        {
            var cart = new Cart
            {
                Id = 1,
                StocksOnHold = new List<StockOnHold>(),
                UserId = "id"
            };

            const string Error = "err";

            _cartRepository.Setup(x => x.GetCartByUserId(It.IsAny<string>(), It.IsAny<Func<Cart, Cart>>()))
                .Returns(() => throw new Exception(Error));

            var request = new AddToCartRequest
            {
                Quantity = 1,
                StockId = 2,
                UserId = "id"
            };

            var res = await _service.AddToCartAsync(request);

            Assert.False(res.Success);
            Assert.Equal(Error, res.Error);
            Assert.DoesNotContain(cart.StocksOnHold, x => x.StockId == request.StockId);
        }

        [Fact]
        public void GetCartByUserId_Success()
        {
            var cart = new Cart
            {
                Id = 1
            };

            _cartRepository.Setup(x => x.GetCartByUserId(It.IsAny<string>(), It.IsAny<Func<Cart, CartModel>>()))
                .Returns((string id, Func<Cart, CartModel> selector) => selector(cart));

            _cartFactory.Setup(x => x.CreateModel(It.IsAny<Cart>()))
                .Returns((Cart cart) => new CartModel { Id = cart.Id });

            _responseFactory.Setup(x => x.CreateSuccess(It.IsAny<CartModel>()))
                .Returns((CartModel data) => new DataResponseModel<CartModel> { Success = true, Data = data });

            var res = _service.GetCartByUserId("id");

            Assert.True(res.Success);
            Assert.NotNull(res.Data);
            Assert.Equal(cart.Id, res.Data.Id);
        }

        [Fact]
        public void GetCartByUserId_CartNotFound_Fail()
        {

            _cartRepository.Setup(x => x.GetCartByUserId(It.IsAny<string>(), It.IsAny<Func<Cart, CartModel>>()))
                .Returns(() => null);

            _responseFactory.Setup(x => x.CreateFailure<CartModel>(It.IsAny<string>()))
                .Returns((string msg) => new DataResponseModel<CartModel> { Success = false, Error = msg, Data = null });

            _cartFactory.Setup(x => x.CreateModel(It.IsAny<Cart>()))
                .Returns((Cart cart) => new CartModel { Id = cart.Id });

            var res = _service.GetCartByUserId("id");

            Assert.False(res.Success);
            Assert.Null(res.Data);
        }

        [Fact]
        public void GetCartItemsCount_Success()
        {
            const int Count = 10;

            _cartRepository.Setup(x => x.GetCartItemsCountByUserId(It.IsAny<string>()))
                .Returns(Count);

            _responseFactory.Setup(x => x.CreateSuccess<int>(It.IsAny<int>()))
                .Returns((int count) => new DataResponseModel<int> { Success = true, Data = count });

            var res = _service.GetCartItemsCount("id");

            Assert.True(res.Success);
            Assert.Equal(Count, res.Data);
        }

        [Fact]
        public async Task RemoveFromCartAsync_Success()
        {
            var cart = new Cart
            {
                Id = 1,
                StocksOnHold = new List<StockOnHold>
                {
                    new StockOnHold { Id = 1, },
                    new StockOnHold { Id = 2, },
                    new StockOnHold { Id = 3, },
                },
            };

            _stockOnHoldRepository.Setup(x => x.DeleteStockOnHoldByIdAsync(It.IsAny<int>()))
                .Callback((int id) => cart.StocksOnHold.Remove(cart.StocksOnHold.First(x => x.Id == id)));

            const int Id = 2;
            var res = await _service.RemoveFromCartAsync(Id);

            Assert.True(res.Success);
            Assert.DoesNotContain(cart.StocksOnHold, x => x.Id == Id);
        }

        [Fact]
        public async Task RemoveFromCartAsync_ExceptionThrown_Fail()
        {
            var cart = new Cart
            {
                Id = 1,
                StocksOnHold = new List<StockOnHold>
                {
                    new StockOnHold { Id = 1, },
                    new StockOnHold { Id = 2, },
                    new StockOnHold { Id = 3, },
                },
            };

            const string Error = "error";

            _stockOnHoldRepository.Setup(x => x.DeleteStockOnHoldByIdAsync(It.IsAny<int>()))
                .Callback((int id) => throw new Exception(Error));

            const int Id = 2;
            var res = await _service.RemoveFromCartAsync(Id);

            Assert.False(res.Success);
            Assert.Equal(Error, res.Error);
            Assert.Contains(cart.StocksOnHold, x => x.Id == Id);
        }
    }
}
