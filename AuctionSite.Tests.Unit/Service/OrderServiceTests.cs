﻿using AuctionSite.Application;
using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.Order;
using AuctionSite.Models.Order.Request;
using AuctionSite.Models.Response;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSite.Tests.Unit.Service
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderFactory> _orderFactory;
        private readonly Mock<IResponseFactory> _responseFactory;
        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly Mock<IStockOnHoldRepository> _stockOnHoldRepository;
        private readonly OrderService _service;

        public OrderServiceTests() 
        {
            _orderFactory = new Mock<IOrderFactory>();
            _responseFactory = new Mock<IResponseFactory>();
            _orderRepository = new Mock<IOrderRepository>();
            _stockOnHoldRepository = new Mock<IStockOnHoldRepository>();

            _responseFactory.Setup(x => x.CreateSuccess())
                .Returns(new ResponseModel { Success = true });

            _responseFactory.Setup(x => x.CreateFailure(It.IsAny<string>()))
                .Returns((string msg) => new ResponseModel { Success = false, Error = msg });

            _service = new OrderService(_orderFactory.Object, _orderRepository.Object, _responseFactory.Object, _stockOnHoldRepository.Object);
        }

        [Fact]
        public async Task AddOrderAsync_Success()
        {
            var request = new AddOrderRequest
            {
                CartId = 2,
                Address = "addr",
                City = "krk"
            };

            _orderFactory.Setup(x => x.Create(It.IsAny<AddOrderRequest>()))
                .Returns((AddOrderRequest request) => new Order
                {
                    Address = request.Address,
                    City = request.City,
                });

            _orderFactory.Setup(x => x.CreateStock(It.IsAny<StockOnHold>(), It.IsAny<int>()))
                .Returns((StockOnHold stock, int id) => new OrderStock { OrderId = id, Quantity = stock.Quantity, StockId = stock.StockId });

            var orders = new List<Order>();
            var orderStocks = new List<OrderStock>();
            var stocksOnHold = new List<StockOnHold>
            {
                new StockOnHold { Quantity = 1 },
                new StockOnHold { Quantity = 2 },
                new StockOnHold { Quantity = 3 },
            };

            _stockOnHoldRepository.Setup(x => x.GetStocksOnHoldByCartId(It.IsAny<int>(), It.IsAny<Func<StockOnHold, StockOnHold>>()))
                .Returns(stocksOnHold);

            _orderRepository.Setup(x => x.AddOrderAsync(It.IsAny<Order>()))
                .Callback((Order order) => orders.Add(order));

            _orderRepository.Setup(x => x.AddOrderStocksAsync(It.IsAny<IEnumerable<OrderStock>>(), It.IsAny<IEnumerable<int>>()))
                .Callback((IEnumerable<OrderStock> stocks, IEnumerable<int> _) => orderStocks.AddRange(stocks));

            var res = await _service.AddOrderAsync(request);

            Assert.True(res.Success);
            Assert.Contains(orders, x => x.Address == request.Address);
            Assert.Equivalent(orderStocks.Select(x => x.Quantity), stocksOnHold.Select(x => x.Quantity));
        }

        [Fact]
        public async Task AddOrderAsync_ExceptionThrown_Fail()
        {
            var request = new AddOrderRequest
            {
                CartId = 2,
                Address = "addr",
                City = "krk"
            };

            const string Error = "Error";

            _orderFactory.Setup(x => x.Create(It.IsAny<AddOrderRequest>()))
                .Callback(() => throw new Exception(Error));
            var res = await _service.AddOrderAsync(request);

            Assert.False(res.Success);
            Assert.Equal(Error, res.Error);
        }

        [Fact]
        public async Task GetOrderByIdAsync_Success()
        {
            var order = new Order { Id = 1, Address = "addr" };

            _orderFactory.Setup(x => x.CreateModel(It.IsAny<Order>()))
                .Returns((Order order) => new OrderModel { Address = order.Address, Id = order.Id });

            _orderRepository.Setup(x => x.GetOrderById(It.IsAny<int>(), It.IsAny<Func<Order, OrderModel>>()))
                .Returns((int _, Func<Order, OrderModel> selector) => selector(order));

            _responseFactory.Setup(x => x.CreateSuccess(It.IsAny<OrderModel>()))
                .Returns((OrderModel data) => new DataResponseModel<OrderModel> { Success = true, Data = data });

            var res = await _service.GetOrderByIdAsync(order.Id);

            Assert.True(res.Success);
            Assert.Equal(res.Data.Address, order.Address);
        }

        [Fact]
        public async Task GetOrderByIdAsync_NotFound_Fail()
        {
            var order = new Order { Id = 1, Address = "addr" };

            _orderFactory.Setup(x => x.CreateModel(It.IsAny<Order>()))
                .Returns((Order order) => new OrderModel { Address = order.Address, Id = order.Id });

            _orderRepository.Setup(x => x.GetOrderById(It.IsAny<int>(), It.IsAny<Func<Order, OrderModel>>()))
                .Returns(() => null);

            _responseFactory.Setup(x => x.CreateFailure<OrderModel>(It.IsAny<string>()))
                .Returns(new DataResponseModel<OrderModel> { Success = false, Data = null });

            var res = await _service.GetOrderByIdAsync(order.Id);

            Assert.False(res.Success);
            Assert.Null(res.Data);
        }

        [Fact]
        public async Task GetOrdersByUserId_Success()
        {
            var orders = new List<Order>
            {
                new Order { Id = 1, CreationDate = DateTime.Now },
                new Order { Id = 2, CreationDate = DateTime.Now },
                new Order { Id = 3, CreationDate = DateTime.Now },
                new Order { Id = 4, CreationDate = DateTime.Now },
                new Order { Id = 5, CreationDate = DateTime.Now },
            };

            _orderFactory.Setup(x => x.CreateListItem(It.IsAny<Order>()))
                .Returns((Order order) => new OrderListItem { Id = order.Id, Created = order.CreationDate.ToString() });

            _orderRepository.Setup(x => x.GetOrdersByUserId(It.IsAny<string>(), It.IsAny<Func<Order, OrderListItem>>()))
                .Returns((string _, Func<Order, OrderListItem> selector) => orders.Select(selector));

            _responseFactory.Setup(x => x.CreateSuccess(It.IsAny<IEnumerable<OrderListItem>>()))
                .Returns((IEnumerable<OrderListItem> data) => new DataResponseModel<IEnumerable<OrderListItem>> { Success = true, Data = data });

            var res = await _service.GetOrdersByUserIdAsync("id");

            Assert.True(res.Success);
            Assert.Equivalent(res.Data.Select(x => x.Id), orders.Select(x => x.Id));
        }
    }
}