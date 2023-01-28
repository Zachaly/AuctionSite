using AuctionSite.Models.User.Request;
using FluentValidation;
namespace AuctionSite.Models.User.Validator
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserName).MaximumLength(50).MinimumLength(5);
            RuleFor(x => x.Address).MaximumLength(50);
            RuleFor(x => x.City).MaximumLength(50);
            RuleFor(x => x.Country).MaximumLength(25);
            RuleFor(x => x.LastName).MaximumLength(60);
            RuleFor(x => x.FirstName).MaximumLength(50);
            RuleFor(x => x.PhoneNumber).MaximumLength(20);
            RuleFor(x => x.PostalCode).MaximumLength(20);
        }
    }
}
