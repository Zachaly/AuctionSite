using AuctionSite.Database.Repository;
using AuctionSite.Domain.Entity;

namespace AuctionSite.Tests.Unit.Repository
{
    public class OrderRepositoryTests : DatabaseTest
    {
        private readonly OrderRepository _repository;

        public OrderRepositoryTests()
        {
            _repository = new OrderRepository(_dbContext);
        }

        [Fact]
        public async Task AddOrderAsync()
        {
            var order = new Order
            {
                Address = "addr",
                CreationDate = DateTime.Now,
                Email = "email@email.com",
                PhoneNumber = "1234567890",
                PostalCode = "1234567890",
                Name = "name",
                City = "krk",
                UserId = "userId",
            };

            await _repository.AddOrderAsync(order);

            Assert.Contains(_dbContext.Order, x => x.Address == order.Address && x.CreationDate == order.CreationDate);
        }

        [Fact]
        public async Task AddOrderStocksAsync()
        {
            var stocksOnHold = new List<StockOnHold>
            {
                new StockOnHold { Id = 1 },
                new StockOnHold { Id = 2 },
                new StockOnHold { Id = 3 },
                new StockOnHold { Id = 4 },
                new StockOnHold { Id = 5 },
                new StockOnHold { Id = 6 },
            };

            var orderStocks = new List<OrderStock>
            {
                new OrderStock { OrderId = 1, Quantity = 2 },
                new OrderStock { OrderId = 1, Quantity = 3 },
                new OrderStock { OrderId = 1, Quantity = 4 },
                new OrderStock { OrderId = 1, Quantity = 5 },
            };

            var stockOnHoldIds = stocksOnHold.Select(x => x.Id).Take(4);

            AddContent(stocksOnHold);

            await _repository.AddOrderStocksAsync(orderStocks, stockOnHoldIds);

            Assert.Contains(_dbContext.OrderStock, x => orderStocks.Any(y => y.Quantity == x.Quantity));
            Assert.DoesNotContain(_dbContext.StockOnHold, x => stockOnHoldIds.Contains(x.Id));
        }

        [Fact]
        public async Task GetOrderById()
        {
            var order = new Order
            {
                Id = 1,
                CreationDate = DateTime.Now,
                Address = "addr",
                Email = "email@email.com",
                PhoneNumber = "1234567890",
                PostalCode = "1234567890",
                Name = "name",
                City = "krk",
                UserId = "userId",
                PaymentId = "1234567890123",
                Stocks = new List<OrderStock>
                {
                    new OrderStock
                    {
                        Stock = new Stock { Value = "val" }
                    }
                }
            };

            AddContent(order);

            const int Id = 1;

            var res = _repository.GetOrderById(Id, x => x);

            Assert.NotNull(res);
            Assert.Equal(Id, res.Id);
            Assert.NotNull(res.Stocks);
        }

        [Fact]
        public async Task GetOrdersByUserId()
        {
            var orders = new List<Order>
            {
                new Order { Id = 1, UserId = "id2", Address = "addr", City = "krk", Email = "email@email.com", Name = "name", PhoneNumber = "1234", PostalCode = "12345" },
                new Order { Id = 2, UserId = "id", Address = "addr", City = "krk", Email = "email@email.com", Name = "name", PhoneNumber = "1234", PostalCode = "12345" },
                new Order { Id = 3, UserId = "id1", Address = "addr", City = "krk", Email = "email@email.com", Name = "name", PhoneNumber = "1234", PostalCode = "12345" },
                new Order { Id = 4, UserId = "id", Address = "addr", City = "krk", Email = "email@email.com", Name = "name", PhoneNumber = "1234", PostalCode = "12345" },
                new Order { Id = 5, UserId = "id4", Address = "addr", City = "krk", Email = "email@email.com", Name = "name", PhoneNumber = "1234", PostalCode = "12345" },
            };

            AddContent(orders);

            const string Id = "id";

            var res = _repository.GetOrdersByUserId(Id, x => x);

            Assert.Equivalent(orders.Where(x => x.UserId == Id).Select(x => x.Id), res.Select(x => x.Id));
        }
    }
}
