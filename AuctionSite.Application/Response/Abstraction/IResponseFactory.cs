using AuctionSite.Models.Response;
using FluentValidation.Results;

namespace AuctionSite.Application.Abstraction
{
    public interface IResponseFactory
    {
        DataResponseModel<T> CreateSuccess<T>(T data);
        DataResponseModel<T> CreateFailure<T>(string errorMessage);
        DataResponseModel<T> CreateValidationError<T>(ValidationResult validationResult);

        ResponseModel CreateSuccess();
        ResponseModel CreateFailure(string errorMessage);
        ResponseModel CreateValidationError(ValidationResult validationResult);

        ResponseModel CreateSuccessWithCreatedId(int newId);
    }
}
