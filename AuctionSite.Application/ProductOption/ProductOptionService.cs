using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Util;
using AuctionSite.Models.ProductOption.Request;
using AuctionSite.Models.ProductOption.Validator;
using AuctionSite.Models.Response;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace AuctionSite.Application
{
    [Implementation(typeof(IProductOptionService))]
    public class ProductOptionService : IProductOptionService
    {
        private readonly IResponseFactory _responseFactory;
        private readonly IProductOptionFactory _productOptionFactory;
        private readonly IProductOptionRepository _productOptionRepository;

        public ProductOptionService(IResponseFactory responseFactory, IProductOptionFactory productOptionFactory,
            IProductOptionRepository productOptionRepository)
        {
            _responseFactory = responseFactory;
            _productOptionFactory = productOptionFactory;
            _productOptionRepository = productOptionRepository;
        }

        public async Task<ResponseModel> AddProductOptionAsync(AddProductOptionRequest request)
        {
            var validation = new AddProductOptionRequestValidator().Validate(request);

            if (!validation.IsValid)
            {
                return _responseFactory.CreateValidationError(validation);
            }

            try
            {
                var option = _productOptionFactory.Create(request);

                await _productOptionRepository.AddProductOptionAsync(option);

                return _responseFactory.CreateSuccess();
            }
            catch (Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }

        public async Task<ResponseModel> DeleteProductOptionByIdAsync(int id)
        {
            try
            {
                await _productOptionRepository.DeleteProductOptionByIdAsync(id);

                return _responseFactory.CreateSuccess();
            }
            catch(Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }
    }
}
