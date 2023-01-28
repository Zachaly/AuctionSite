using AuctionSite.Models.ProductOption.Request;
using FluentValidation;

namespace AuctionSite.Models.ProductOption.Validator
{
    public class AddProductOptionRequestValidator : AbstractValidator<AddProductOptionRequest>
    {
        public AddProductOptionRequestValidator()
        {
            RuleFor(x => x.Value).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
