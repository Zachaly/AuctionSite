using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Response;
using AuctionSite.Models.SaveList;
using AuctionSite.Models.SaveList.Request;

namespace AuctionSite.Application
{
    [Implementation(typeof(IListService))]
    public class ListService : IListService
    {
        private readonly IListRepository _listRepository;
        private readonly IListFactory _listFactory;
        private readonly IResponseFactory _responseFactory;

        public ListService(IListRepository listRepository, IListFactory listFactory, IResponseFactory responseFactory)
        {
            _listRepository = listRepository;
            _listFactory = listFactory;
            _responseFactory = responseFactory;
        }

        public Task<ResponseModel> AddListAsync(AddListRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> DeleteListByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public DataResponseModel<ListModel> GetListById(int id)
        {
            throw new NotImplementedException();
        }

        public DataResponseModel<IEnumerable<ListListModel>> GetUserLists(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
