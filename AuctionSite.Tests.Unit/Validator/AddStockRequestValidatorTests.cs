using AuctionSite.Models.Stock.Request;
using AuctionSite.Models.Stock.Validator;

namespace AuctionSite.Tests.Unit.Validator
{
    public class AddStockRequestValidatorTests
    {
        private readonly AddStockRequestValidator _validator;

        public AddStockRequestValidatorTests()
        {
            _validator = new AddStockRequestValidator();
        }

        [Fact]
        public void CorrectData_PassesValidation()
        {
            var request = new AddStockRequest
            {
                ProductId = 1,
                Quantity = 2,
                Value = "val"
            };

            var result = _validator.Validate(request);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void OnlyRequiredFields_PassesValidation()
        {
            var request = new AddStockRequest
            {
                Quantity = 2,
                Value = "val"
            };

            var result = _validator.Validate(request);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void QuantityBelowZero_FailsValidation()
        {
            var request = new AddStockRequest
            {
                Quantity = -1,
                Value = "val"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ValueExceedsMaxLength_FailsValidation()
        {
            var request = new AddStockRequest
            {
                Quantity = 1,
                Value = new string('a', 21)
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }
    }
}
