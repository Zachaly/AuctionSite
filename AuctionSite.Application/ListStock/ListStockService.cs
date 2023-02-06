using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Util;
using AuctionSite.Models.ListStock.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Application
{
    [Implementation(typeof(IListStockService))]
    public class ListStockService : IListStockService
    {
        private readonly IListStockRepository _listStockRepository;
        private readonly IListStockFactory _listStockFactory;
        private readonly IResponseFactory _responseFactory;

        public ListStockService(IListStockRepository listStockRepository, IListStockFactory listStockFactory, IResponseFactory responseFactory)
        {
            _listStockRepository = listStockRepository;
            _listStockFactory = listStockFactory;
            _responseFactory = responseFactory;
        }
        public async Task<ResponseModel> AddListStockAsync(AddListStockRequest request)
        {
            try
            {
                var stock = _listStockFactory.Create(request);

                await _listStockRepository.AddListStockAsync(stock);

                return _responseFactory.CreateSuccess();
            }
            catch (Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }

        public async Task<ResponseModel> DeleteListStockByIdAsync(int id)
        {
            try
            {
                await _listStockRepository.DeleteListStokcByIdAsync(id);

                return _responseFactory.CreateSuccess();
            }
            catch(Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }
    }
}
