using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Util;
using AuctionSite.Models.ListStock.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Application.ListStock
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
        public Task<ResponseModel> AddListStockAsync(AddListStockRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> DeleteListStockByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
