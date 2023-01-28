using AuctionSite.Models.ProductOption.Request;
using FluentValidation;

namespace AuctionSite.Models.ProductOption.Validator
{
    public class AddProductOptionValidator : AbstractValidator<AddProductOptionRequest>
    {
        public AddProductOptionValidator()
        {
            RuleFor(x => x.Value).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
