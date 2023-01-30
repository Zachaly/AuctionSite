using AuctionSite.Models.Stock.Request;
using FluentValidation;

namespace AuctionSite.Models.Stock.Validator
{
    public class AddStockRequestValidator : AbstractValidator<AddStockRequest>
    {
        public AddStockRequestValidator()
        {
            RuleFor(x => x.Value).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
