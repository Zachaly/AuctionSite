using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Stock.Request;
using AuctionSite.Models.Stock.Validator;
using AuctionSite.Models.Response;

namespace AuctionSite.Application
{
    [Implementation(typeof(IStockService))]
    public class StockService : IStockService
    {
        private readonly IResponseFactory _responseFactory;
        private readonly IStockFactory _stockFactory;
        private readonly IStockRepository _stockRepository;

        public StockService(IResponseFactory responseFactory, IStockFactory stockFactory,
            IStockRepository stockRepository)
        {
            _responseFactory = responseFactory;
            _stockFactory = stockFactory;
            _stockRepository = stockRepository;
        }

        public async Task<ResponseModel> AddStockAsync(AddStockRequest request)
        {
            var validation = new AddStockRequestValidator().Validate(request);

            if (!validation.IsValid)
            {
                return _responseFactory.CreateValidationError(validation);
            }

            try
            {
                var stock = _stockFactory.Create(request);

                await _stockRepository.AddStockAsync(stock);

                return _responseFactory.CreateSuccess();
            }
            catch (Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }

        public async Task<ResponseModel> DeleteStockByIdAsync(int id)
        {
            try
            {
                await _stockRepository.DeleteStockByIdAsync(id);

                return _responseFactory.CreateSuccess();
            }
            catch(Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }

        public async Task<ResponseModel> UpdateStockAsync(UpdateStockRequest request)
        {
            try
            {
                var validaton = new UpdateStockValidator().Validate(request);
                if (!validaton.IsValid)
                {
                    return _responseFactory.CreateValidationError(validaton);
                }

                var stock = _stockRepository.GetStockById(request.Id, stock => stock);

                stock.Value = request.Value ?? stock.Value;
                stock.Quantity = request.Quantity ?? stock.Quantity;

                await _stockRepository.UpdateStockAsync(stock);

                return _responseFactory.CreateSuccess();
            }
            catch (Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }
    }
}
