using AuctionSite.Models.User.Request;
using FluentValidation;

namespace AuctionSite.Models.User.Validator
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {

        }
    }
}
