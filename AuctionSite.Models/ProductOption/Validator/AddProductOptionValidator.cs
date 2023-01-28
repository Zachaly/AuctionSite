using AuctionSite.Models.ProductOption.Request;
using FluentValidation;

namespace AuctionSite.Models.ProductOption.Validator
{
    public class AddProductOptionValidator : AbstractValidator<AddProductOptionRequest>
    {
        public AddProductOptionValidator()
        {

        }
    }
}
