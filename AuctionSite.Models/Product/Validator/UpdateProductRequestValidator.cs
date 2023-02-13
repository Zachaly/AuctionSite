using AuctionSite.Models.Product.Request;
using FluentValidation;

namespace AuctionSite.Models.Product.Validator
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.StockName).MaximumLength(25);
            RuleFor(x => x.Description).MaximumLength(200);
            RuleFor(x => x.Name).MaximumLength(50);
        }
    }
}
