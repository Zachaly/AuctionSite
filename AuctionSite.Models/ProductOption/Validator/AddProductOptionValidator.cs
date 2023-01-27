using AuctionSite.Models.Product.Request;
using FluentValidation;

namespace AuctionSite.Models.ProductOption.Validator
{
    public class AddProductOptionValidator : AbstractValidator<AddProductRequest>
    {
        public AddProductOptionValidator()
        {

        }
    }
}
