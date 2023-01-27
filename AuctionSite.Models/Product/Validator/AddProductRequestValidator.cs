using AuctionSite.Models.Product.Request;
using FluentValidation;

namespace AuctionSite.Models.Product.Validator
{
    public class AddProductRequestValidator : AbstractValidator<AddProductRequest>
    {
        public AddProductRequestValidator()
        {

        }
    }
}
