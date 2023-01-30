using AuctionSite.Models.Product.Request;
using AuctionSite.Models.Product.Validator;
using AuctionSite.Models.Stock.Request;
using AuctionSite.Models.Stock.Validator;

namespace AuctionSite.Tests.Unit.Validator
{
    public class AddProductRequestValidatorTests
    {
        private readonly AddProductRequestValidator _validator;

        public AddProductRequestValidatorTests()
        {
            _validator = new AddProductRequestValidator(new AddStockRequestValidator());
        }

        [Fact]
        public void CorrectData_PassesValidation()
        {
            var request = new AddProductRequest
            {
                Description = "description",
                Name = "name",
                StockName = "opt name",
                Stocks = new AddStockRequest[] { new AddStockRequest() { Quantity = 1, Value = "val" } },
                Price = 123,
                UserId = "id"
            };

            var result = _validator.Validate(request);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void LackOfRequiredField_FailsValidation()
        {
            var request = new AddProductRequest
            {
                Stocks = new AddStockRequest[] { new AddStockRequest() { Quantity = 1, Value = "val" } },
                Price = 123,
                UserId = "id"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void DescriptionExceedsMaxLength_FailsValidation()
        {
            var request = new AddProductRequest
            {
                Description = new string('a', 201),
                Name = "name",
                StockName = "opt name",
                Stocks = new AddStockRequest[] { new AddStockRequest() { Quantity = 1, Value = "val" } },
                Price = 123,
                UserId = "id"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void NameExceedsMaxLength_FailsValidation()
        {
            var request = new AddProductRequest
            {
                Description = "description",
                Name = new string('a', 51),
                StockName = "opt name",
                Stocks = new AddStockRequest[] { new AddStockRequest() { Quantity = 1, Value = "val" } },
                Price = 123,
                UserId = "id"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void StockNameExceedsMaxLength_FailsValidation()
        {
            var request = new AddProductRequest
            {
                Description = "description",
                Name = "name",
                StockName = new string('a', 26),
                Stocks = new AddStockRequest[] { new AddStockRequest() { Quantity = 1, Value = "val" } },
                Price = 123,
                UserId = "id"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void NoStocks_FailsValidation()
        {
            var request = new AddProductRequest
            {
                Description = "description",
                Name = new string('a', 51),
                StockName = "opt name",
                Stocks = new AddStockRequest[] { },
                Price = 123,
                UserId = "id"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void PriceEqualOrBelowZero_FailsValidation(int price)
        {
            var request = new AddProductRequest
            {
                Description = "description",
                Name = new string('a', 51),
                StockName = "opt name",
                Stocks = new AddStockRequest[] { new AddStockRequest() { Quantity = 1, Value = "val" } },
                Price = price,
                UserId = "id"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }
    }
}
