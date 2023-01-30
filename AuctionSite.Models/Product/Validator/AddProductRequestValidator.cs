using AuctionSite.Models.Product.Request;
using AuctionSite.Models.Stock.Request;
using FluentValidation;

namespace AuctionSite.Models.Product.Validator
{
    public class AddProductRequestValidator : AbstractValidator<AddProductRequest>
    {
        public AddProductRequestValidator(AbstractValidator<AddStockRequest> stockValidator)
        {
            
            RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.StockName).NotEmpty().MaximumLength(25);
            RuleFor(x => x.Stocks).NotEmpty().Must(x => x.All(opt => stockValidator.Validate(opt).IsValid));
            RuleFor(x => x.Description).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}
