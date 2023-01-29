using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AuctionSite.Application.Command
{
    public class SaveProductImagesCommand : IRequest<ResponseModel>
    {
        public IEnumerable<IFormFile> Images { get; set; }
        public int ProductId { get; set; }
    }

    public class SaveProductImagesHandler : IRequestHandler<SaveProductImagesCommand, ResponseModel>
    {
        public SaveProductImagesHandler(IProductImageRepository productImageRepository, IResponseFactory responseFactory)
        {

        }

        public Task<ResponseModel> Handle(SaveProductImagesCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
