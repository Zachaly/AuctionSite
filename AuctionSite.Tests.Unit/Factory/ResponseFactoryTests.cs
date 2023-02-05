using AuctionSite.Application;
using FluentValidation.Results;

namespace AuctionSite.Tests.Unit.Factory
{
    public class ResponseFactoryTests
    {
        private readonly ResponseFactory _factory;

        public ResponseFactoryTests()
        {
            _factory = new ResponseFactory();
        }

        [Fact]
        public void CreateFail()
        {
            const string ErrorMessage = "Error";

            var response = _factory.CreateFailure(ErrorMessage);

            Assert.Equal(ErrorMessage, response.Error);
            Assert.False(response.Success);
        }

        [Fact]
        public void CreateSuccess()
        {
            var response = _factory.CreateSuccess();

            Assert.True(response.Success);
        }

        [Fact]
        public void CreateValidationError()
        {
            var validation = new ValidationResult
            {
                Errors = new List<ValidationFailure>
                {
                    new ValidationFailure
                    {
                        PropertyName = "error1",
                        ErrorMessage = "error1msg"
                    },
                    new ValidationFailure
                    {
                        PropertyName = "error2",
                        ErrorMessage = "error2msg"
                    }
                }
            };

            var response = _factory.CreateValidationError(validation);

            Assert.False(response.Success);
            Assert.All(response.ValidationErrors.Keys, x => validation.ToDictionary().Keys.Contains(x));
            Assert.Equal(validation.ToDictionary().Count, response.ValidationErrors.Count);
        }

        [Fact]
        public void CreateFail_Data()
        {
            const string ErrorMessage = "Error";

            var response = _factory.CreateFailure<int>(ErrorMessage);

            Assert.Equal(ErrorMessage, response.Error);
            Assert.False(response.Success);
            Assert.Equal(default(int), response.Data);
        }

        [Fact]
        public void CreateSuccess_Data()
        {
            const int Data = 1;

            var response = _factory.CreateSuccess(Data);

            Assert.True(response.Success);
            Assert.Equal(Data, response.Data);
        }

        [Fact]
        public void CreateValidationError_Data()
        {
            var validation = new ValidationResult
            {
                Errors = new List<ValidationFailure>
                {
                    new ValidationFailure
                    {
                        PropertyName = "error1",
                        ErrorMessage = "error1msg"
                    },
                    new ValidationFailure
                    {
                        PropertyName = "error2",
                        ErrorMessage = "error2msg"
                    }
                }
            };

            var response = _factory.CreateValidationError<int>(validation);

            Assert.False(response.Success);
            Assert.All(response.ValidationErrors.Keys, x => validation.ToDictionary().Keys.Contains(x));
            Assert.Equal(validation.ToDictionary().Count, response.ValidationErrors.Count);
            Assert.Equal(default(int), response.Data);
        }

        [Fact]
        public void CreateSuccessWithCreatedId()
        {
            const int Id = 1;

            var response = _factory.CreateSuccessWithCreatedId(Id);

            Assert.Equal(Id, response.NewEntityId);
            Assert.True(response.Success);
        }
    }
}
