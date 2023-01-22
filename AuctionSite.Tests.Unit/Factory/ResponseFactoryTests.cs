using AuctionSite.Application;
using FluentValidation.Results;

namespace AuctionSite.Tests.Unit.Factory
{
    public class ResponseFactoryTests
    {
        [Fact]
        public void CreateFail()
        {
            var factory = new ResponseFactory();

            const string ErrorMessage = "Error";

            var response = factory.CreateFailure(ErrorMessage);

            Assert.Equal(ErrorMessage, response.Error);
            Assert.False(response.Success);
        }

        [Fact]
        public void CreateSuccess()
        {
            var factory = new ResponseFactory();

            var response = factory.CreateSuccess();

            Assert.True(response.Success);
        }

        [Fact]
        public void CreateValidationError()
        {
            var factory = new ResponseFactory();

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

            var response = factory.CreateValidationError(validation);

            Assert.False(response.Success);
            Assert.All(response.ValidationErrors.Keys, x => validation.ToDictionary().Keys.Contains(x));
            Assert.Equal(validation.ToDictionary().Count, response.ValidationErrors.Count);
        }

        [Fact]
        public void CreateFail_Data()
        {
            var factory = new ResponseFactory();

            const string ErrorMessage = "Error";

            var response = factory.CreateFailure<int>(ErrorMessage);

            Assert.Equal(ErrorMessage, response.Error);
            Assert.False(response.Success);
            Assert.Equal(default(int), response.Data);
        }

        [Fact]
        public void CreateSuccess_Data()
        {
            var factory = new ResponseFactory();

            const int Data = 1;

            var response = factory.CreateSuccess(Data);

            Assert.True(response.Success);
            Assert.Equal(Data, response.Data);
        }

        [Fact]
        public void CreateValidationError_Data()
        {
            var factory = new ResponseFactory();

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

            var response = factory.CreateValidationError<int>(validation);

            Assert.False(response.Success);
            Assert.All(response.ValidationErrors.Keys, x => validation.ToDictionary().Keys.Contains(x));
            Assert.Equal(validation.ToDictionary().Count, response.ValidationErrors.Count);
            Assert.Equal(default(int), response.Data);
        }
    }
}
