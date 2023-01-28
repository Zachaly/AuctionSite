using AuctionSite.Models.Product.Request;
using AuctionSite.Models.ProductOption.Request;
using FluentValidation;

namespace AuctionSite.Models.Product.Validator
{
    public class AddProductRequestValidator : AbstractValidator<AddProductRequest>
    {
        public AddProductRequestValidator(AbstractValidator<AddProductOptionRequest> productOptionValidator)
        {
            
            RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.OptionName).NotEmpty().MaximumLength(25);
            RuleFor(x => x.Options).NotEmpty().Must(x => x.All(opt => productOptionValidator.Validate(opt).IsValid));
            RuleFor(x => x.Description).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}
