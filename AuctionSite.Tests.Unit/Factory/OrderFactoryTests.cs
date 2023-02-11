using AuctionSite.Application;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Enum;
using AuctionSite.Models.Order.Request;
using FluentValidation;

namespace AuctionSite.Tests.Unit.Factory
{
    public class OrderFactoryTests
    {
        private readonly OrderFactory _factory;

        public OrderFactoryTests()
        {
            _factory = new OrderFactory();
        }

        [Fact]
        public void Create()
        {
            var request = new AddOrderRequest
            {
                Address = "addr 12",
                City = "krk",
                Email = "email@email.com",
                Name = "name",
                PaymentId = "1234",
                PhoneNumber = "1234567890",
                PostalCode = "12345",
                UserId = "userId",
            };

            var order = _factory.Create(request);

            Assert.Equal(request.Address, order.Address);
            Assert.Equal(request.City, order.City);
            Assert.Equal(request.Email, order.Email);
            Assert.Equal(request.Name, order.Name);
            Assert.Equal(request.PaymentId, order.PaymentId);
            Assert.Equal(request.PhoneNumber, order.PhoneNumber);
            Assert.Equal(request.PostalCode, order.PostalCode);
            Assert.Equal(request.UserId, order.UserId);
        }

        [Fact]
        public void CreateListItem()
        {
            var order = new Order { Id = 1, CreationDate = DateTime.Now };

            var item = _factory.CreateListItem(order);

            Assert.Equal(order.Id, item.Id);
            Assert.NotEmpty(item.Created);
        }

        [Fact]
        public void CreateModel()
        {
            var stock = new OrderStock
            {
                Id = 2,
                Quantity = 3,
                Stock = new Stock
                {
                    Product = new Product
                    {
                        Price = 123,
                        Id = 4,
                        Name = "product"
                    },
                    Value = "stock",
                    ProductId = 4,
                },
                RealizationStatus = RealizationStatus.Pending
            };

            var order = new Order
            {
                Address = "addr",
                City = "krk",
                CreationDate = new DateTime(1, 2, 3),
                Email = "email@email.com",
                Id = 1,
                Name = "jon snow",
                PaymentId = "12456",
                PhoneNumber = "1234567890",
                PostalCode = "12345",
                Stocks = new List<OrderStock> { stock },
                UserId = "id"
            };

            var model = _factory.CreateModel(order);
            var item = model.Items.First();

            Assert.Equal(order.Id, model.Id);
            Assert.Equal(order.Address, model.Address);
            Assert.Equal(order.City, model.City);
            Assert.Equal(order.CreationDate.ToString("dd.MM.yyyy"), model.Created);
            Assert.Equal(order.Name, model.Name);
            Assert.Equal(order.PaymentId, model.PaymentId);
            Assert.Equal(order.PhoneNumber, model.PhoneNumber);
            Assert.Equal(order.PostalCode, model.PostalCode);
            Assert.Equal(stock.Quantity, item.Quantity);
            Assert.Equal(stock.Stock.Value, item.StockName);
            Assert.Equal(stock.Stock.Product.Name, item.ProductName);
            Assert.Equal(stock.Stock.ProductId, item.ProductId);
            Assert.Equal(stock.Stock.Product.Price, item.Price);
            Assert.Equal(stock.RealizationStatus, item.Status);
        }

        [Fact]
        public void CreateStock()
        {
            var stockOnHold = new StockOnHold { Quantity = 1, StockId = 2 };

            const int OrderId = 3;

            var stock = _factory.CreateStock(stockOnHold, OrderId);

            Assert.Equal(stockOnHold.Quantity, stock.Quantity);
            Assert.Equal(stockOnHold.StockId, stock.StockId);
            Assert.Equal(OrderId, stock.OrderId);
            Assert.Equal(RealizationStatus.Pending, stock.RealizationStatus);
        }

        [Fact]
        public void CreateProductOrderModel()
        {
            var stock = new OrderStock 
            { 
                Id = 1,
                RealizationStatus = RealizationStatus.Pending,
                Quantity = 2,
                StockId = 3,
                Stock = new Stock
                {
                    Id = 3,
                    Value = "val",
                },
                Order = new Order
                {
                    Name = "johhny",
                    Address = "Addr",
                    City = "krk",
                    CreationDate = DateTime.Now,
                    Email = "email",
                    PaymentId = "payment",
                    PhoneNumber = "1234567890",
                    PostalCode = "12345"
                }
            };

            var model = _factory.CreateModel(stock);

            Assert.Equal(stock.Id, model.Id);
            Assert.Equal(stock.RealizationStatus, model.Status);
            Assert.Equal(stock.Quantity, model.Quantity);
            Assert.Equal(stock.StockId, model.StockId);
            Assert.Equal(stock.Stock.Value, model.StockName);
            Assert.Equal(stock.Order.Name, model.Name);
            Assert.Equal(stock.Order.Address, model.Address);
            Assert.Equal(stock.Order.Email, model.Email);
            Assert.Equal(stock.Order.PaymentId, model.PaymentId);
            Assert.Equal(stock.Order.City, model.City);
            Assert.Equal(stock.Order.CreationDate.ToString("dd.MM.yyyy"), model.CreationDate);
            Assert.Equal(stock.Order.PhoneNumber, model.PhoneNumber);
            Assert.Equal(stock.Order.PostalCode, model.PostalCode);
        }

        [Fact]
        public void CreateManagementItem()
        {
            var stock = new OrderStock
            {
                Id = 2,
                Quantity = 3,
                Stock = new Stock
                {
                    Product = new Product
                    {
                        Price = 123,
                        Id = 4,
                        Name = "product"
                    },
                    Value = "stock",
                    ProductId = 4,
                },
                RealizationStatus = RealizationStatus.Pending,
                Order = new Order
                {
                    CreationDate = DateTime.Now,
                }
            };

            var item = _factory.CreateManagementItem(stock);

            Assert.Equal(stock.Id, item.OrderStockId);
            Assert.Equal(stock.Stock.Value, item.StockName);
            Assert.Equal(stock.RealizationStatus, item.Status);
            Assert.Equal(stock.Quantity, item.Quantity);
            Assert.Equal(stock.Order.CreationDate.ToString("dd.MM.yyyy"), item.Created);
        }
    }
}
