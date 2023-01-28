using AuctionSite.Models.User.Request;
using AuctionSite.Models.User.Validator;

namespace AuctionSite.Tests.Unit.Validator
{
    public class RegisterRequestValidatorTests
    {
        private readonly RegisterRequestValidator _validator;

        public RegisterRequestValidatorTests()
        {
            _validator = new RegisterRequestValidator();
        }

        [Fact]
        public void CorrectData_PassesValidation()
        {
            var request = new RegisterRequest
            {
                Address = "street 21",
                City = "krakow",
                Country = "poland",
                Email = "email@email.com",
                FirstName = "john",
                Gender = Domain.Enum.Gender.Male,
                LastName = "smith",
                Password = "zaq1@WSX",
                PhoneNumber = "1234567890",
                PostalCode = "32-089",
                Username = "user name"
            };

            var result = _validator.Validate(request);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void OnlyRequiredFields_PassesValidation()
        {
            var request = new RegisterRequest
            {
                Email = "email@email.com",
                Password = "zaq1@WSX",
                Username = "user name"
            };

            var result = _validator.Validate(request);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void LackOfRequiredFields_FailsValidation()
        {
            var request = new RegisterRequest
            {
                Email = "email@email.com",
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void PasswordExceedsMaxLength_FailsValidation()
        {
            var request = new RegisterRequest
            {
                Email = "email@email.com",
                Password = new string('a', 101),
                Username = "user name"
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void UserNameExceedsMaxLength_FailsValidation()
        {
            var request = new RegisterRequest
            {
                Email = "email@email.com",
                Password = "zaq1@WSX",
                Username = new string('a', 51)
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void UserNameBelowMinLength_FailsValidation()
        {
            var request = new RegisterRequest
            {
                Email = "email@email.com",
                Password = "zaq1@WSX",
                Username = new string('a', 4)
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void AddressExceedsMaxLength_FailsValidation()
        {
            var request = new RegisterRequest
            {
                Email = "email@email.com",
                Password = "zaq1@WSX",
                Username = "user name",
                Address = new string('a', 51)
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void CityExceedsMaxLength_FailsValidation()
        {
            var request = new RegisterRequest
            {
                Email = "email@email.com",
                Password = "zaq1@WSX",
                Username = "user name",
                City = new string('a', 51)
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void CountryExceedsMaxLength_FailsValidation()
        {
            var request = new RegisterRequest
            {
                Email = "email@email.com",
                Password = "zaq1@WSX",
                Username = "user name",
                Country = new string('a', 25)
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void FirstNameExceedsMaxLength_FailsValidation()
        {
            var request = new RegisterRequest
            {
                Email = "email@email.com",
                Password = "zaq1@WSX",
                Username = "user name",
                LastName = new string('a', 61)
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void LastNameExceedsMaxLength_FailsValidation()
        {
            var request = new RegisterRequest
            {
                Email = "email@email.com",
                Password = "zaq1@WSX",
                Username = "user name",
                LastName = new string('a', 61)
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void PhoneNumberExceedsMaxLength_FailsValidation()
        {
            var request = new RegisterRequest
            {
                Email = "email@email.com",
                Password = "zaq1@WSX",
                Username = "user name",
                PhoneNumber = new string('a', 21)
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void PostalCodeExceedsMaxLength_FailsValidation()
        {
            var request = new RegisterRequest
            {
                Email = "email@email.com",
                Password = "zaq1@WSX",
                Username = "user name",
                PostalCode = new string('a', 21)
            };

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
        }
    }
}
