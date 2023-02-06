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

        public async Task<ResponseModel> AddListAsync(AddListRequest request)
        {
            try
            {
                var list = _listFactory.Create(request);

                await _listRepository.AddListAsync(list);

                return _responseFactory.CreateSuccess();
            }
            catch (Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }

        public async Task<ResponseModel> DeleteListByIdAsync(int id)
        {
            try
            {
                await _listRepository.DeleteListByIdAsync(id);

                return _responseFactory.CreateSuccess();
            }
            catch(Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }

        public DataResponseModel<ListModel> GetListById(int id)
        {
            try
            {
                var data = _listRepository.GetListById(id, list => _listFactory.CreateModel(list));

                return _responseFactory.CreateSuccess(data);
            }
            catch (Exception ex)
            {
                return _responseFactory.CreateFailure<ListModel>(ex.Message);
            }
        }

        public DataResponseModel<IEnumerable<ListListModel>> GetUserLists(string userId)
        {
            var data = _listRepository.GetUserLists(userId, list => _listFactory.CreateListItem(list));

            return _responseFactory.CreateSuccess(data);
        }
    }
}
