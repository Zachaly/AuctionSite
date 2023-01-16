using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Response;
using FluentValidation.Results;

namespace AuctionSite.Application
{
    [Implementation(typeof(IResponseFactory))]
    public class ResponseFactory : IResponseFactory
    {
        public DataResponseModel<T> CreateFailure<T>(string errorMessage)
        {
            throw new NotImplementedException();
        }

        public ResponseModel CreateFailure(string errorMessage)
        {
            throw new NotImplementedException();
        }

        public DataResponseModel<T> CreateSuccess<T>(T data)
        {
            throw new NotImplementedException();
        }

        public ResponseModel CreateSuccess()
        {
            throw new NotImplementedException();
        }

        public DataResponseModel<T> CreateValidationError<T>(ValidationResult validationResult)
        {
            throw new NotImplementedException();
        }

        public ResponseModel CreateValidationError(ValidationResult validationResult)
        {
            throw new NotImplementedException();
        }
    }
}
