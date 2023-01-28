using AuctionSite.Models.Product.Request;
using AuctionSite.Models.Product.Validator;
using AuctionSite.Models.ProductOption.Request;
using AuctionSite.Models.ProductOption.Validator;

namespace AuctionSite.Tests.Unit.Validator
{
    public class AddProductRequestValidatorTests
    {
        private readonly AddProductRequestValidator _validator;

        public AddProductRequestValidatorTests()
        {
            _validator = new AddProductRequestValidator(new AddProductOptionRequestValidator());
        }

        [Fact]
        public void CorrectData_PassesValidation()
        {
            var request = new AddProductRequest
            {
                Description = "description",
                Name = "name",
                OptionName = "opt name",
                Options = new AddProductOptionRequest[] { new AddProductOptionRequest() { Quantity = 1, Value = "val" } },
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
                Options = new AddProductOptionRequest[] { new AddProductOptionRequest() { Quantity = 1, Value = "val" } },
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
                OptionName = "opt name",
                Options = new AddProductOptionRequest[] { new AddProductOptionRequest() { Quantity = 1, Value = "val" } },
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
                OptionName = "opt name",
                Options = new AddProductOptionRequest[] { new AddProductOptionRequest() { Quantity = 1, Value = "val" } },
                Price = 123,
                UserId = "id"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void OptionNameExceedsMaxLength_FailsValidation()
        {
            var request = new AddProductRequest
            {
                Description = "description",
                Name = "name",
                OptionName = new string('a', 26),
                Options = new AddProductOptionRequest[] { new AddProductOptionRequest() { Quantity = 1, Value = "val" } },
                Price = 123,
                UserId = "id"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void NoOptions_FailsValidation()
        {
            var request = new AddProductRequest
            {
                Description = "description",
                Name = new string('a', 51),
                OptionName = "opt name",
                Options = new AddProductOptionRequest[] { },
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
                OptionName = "opt name",
                Options = new AddProductOptionRequest[] { new AddProductOptionRequest() { Quantity = 1, Value = "val" } },
                Price = price,
                UserId = "id"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }
    }
}
