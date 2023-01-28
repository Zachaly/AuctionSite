using AuctionSite.Models.ProductOption.Request;
using AuctionSite.Models.ProductOption.Validator;

namespace AuctionSite.Tests.Unit.Validator
{
    public class AddProductOptionValidatorTests
    {
        private readonly AddProductOptionValidator _validator;

        public AddProductOptionValidatorTests()
        {
            _validator = new AddProductOptionValidator();
        }

        [Fact]
        public void CorrectData_PassesValidation()
        {
            var request = new AddProductOptionRequest
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
            var request = new AddProductOptionRequest
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
            var request = new AddProductOptionRequest
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
            var request = new AddProductOptionRequest
            {
                Quantity = -1,
                Value = new string('a', 21)
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }
    }
}
