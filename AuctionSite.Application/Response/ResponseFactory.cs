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
            => new DataResponseModel<T>
            {
                Error = errorMessage,
                Data = default(T),
                Success = false
            };

        public ResponseModel CreateFailure(string errorMessage)
            => new ResponseModel
            {
                Success = false,
                Error = errorMessage,
            };

        public DataResponseModel<T> CreateSuccess<T>(T data)
            => new DataResponseModel<T> 
            { 
                Success = true,
                Data = data 
            };

        public ResponseModel CreateSuccess()
            => new ResponseModel 
            { 
                Success = true
            };

        public DataResponseModel<T> CreateValidationError<T>(ValidationResult validationResult)
            => new DataResponseModel<T>
            {
                Data = default(T),
                ValidationErrors = validationResult.ToDictionary(),
                Success = false
            };

        public ResponseModel CreateValidationError(ValidationResult validationResult)
            => new ResponseModel
            {
                Success = false,
                ValidationErrors = validationResult.ToDictionary()
            };
    }
}
