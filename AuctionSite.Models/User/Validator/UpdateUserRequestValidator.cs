using AuctionSite.Models.User.Request;
using FluentValidation;
namespace AuctionSite.Models.User.Validator
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {

        }
    }
}
