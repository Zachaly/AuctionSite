using AuctionSite.Models.User.Request;
using AuctionSite.Models.User.Validator;

namespace AuctionSite.Tests.Unit.Validator
{
    public class UpdateUserRequestValidatorTests
    {
        private readonly UpdateUserRequestValidator _validator;

        public UpdateUserRequestValidatorTests()
        {
            _validator = new UpdateUserRequestValidator();
        }

        [Fact]
        public void CorrectData_PassesValidation()
        {
            var request = new UpdateUserRequest
            {
                Address = "street 21",
                City = "krakow",
                Country = "poland",
                FirstName = "john",
                Gender = Domain.Enum.Gender.Male,
                LastName = "smith",
                PhoneNumber = "1234567890",
                PostalCode = "32-089",
                Id = "userid",
                UserName = "user name"
            };

            var result = _validator.Validate(request);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void OnlyRequiredFields_PassesValidation()
        {
            var request = new UpdateUserRequest
            {
                Id = "userid"
            };

            var result = _validator.Validate(request);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void LackOfRequiredFields_FailsValidation()
        {
            var request = new UpdateUserRequest
            {
                
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void UserNameExceedsMaxLength_FailsValidation()
        {
            var request = new UpdateUserRequest
            {
                UserName = new string('a', 51),
                Id = "userid"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void UserNameBelowMinLength_FailsValidation()
        {
            var request = new UpdateUserRequest
            {
                UserName = new string('a', 4),
                Id = "userid"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void AddressExceedsMaxLength_FailsValidation()
        {
            var request = new UpdateUserRequest
            {
                Address = new string('a', 51),
                Id = "userid"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void CityExceedsMaxLength_FailsValidation()
        {
            var request = new UpdateUserRequest
            {
                City = new string('a', 51),
                Id = "userid"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void CountryExceedsMaxLength_FailsValidation()
        {
            var request = new UpdateUserRequest
            {
                Country = new string('a', 25),
                Id = "userid"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void FirstNameExceedsMaxLength_FailsValidation()
        {
            var request = new UpdateUserRequest
            {
                LastName = new string('a', 61),
                Id = "userid"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void LastNameExceedsMaxLength_FailsValidation()
        {
            var request = new UpdateUserRequest
            {
                LastName = new string('a', 61),
                Id = "userid"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void PhoneNumberExceedsMaxLength_FailsValidation()
        {
            var request = new UpdateUserRequest
            {
                PhoneNumber = new string('a', 21),
                Id = "userid"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void PostalCodeExceedsMaxLength_FailsValidation()
        {
            var request = new UpdateUserRequest
            {
                PostalCode = new string('a', 21),
                Id = "userid"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }
    }
}
