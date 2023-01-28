using AuctionSite.Models.User.Request;
using FluentValidation;

namespace AuctionSite.Models.User.Validator
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Username).NotEmpty().MaximumLength(50).MinimumLength(5);
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
