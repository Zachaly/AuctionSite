using AuctionSite.Models.Stock.Request;
using FluentValidation;

namespace AuctionSite.Models.Stock.Validator
{
    public class UpdateStockValidator : AbstractValidator<UpdateStockRequest>
    {
        public UpdateStockValidator()
        {
            RuleFor(x => x.Value).MaximumLength(20);
            RuleFor(x => x.Quantity).GreaterThan(-1);
        }
    }
}
