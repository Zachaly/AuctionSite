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
        private readonly IProductImageRepository _productImageRepository;
        private readonly IResponseFactory _responseFactory;
        private readonly IProductFactory _productFactory;
        private readonly IFileService _fileService;

        public SaveProductImagesHandler(IProductImageRepository productImageRepository, IResponseFactory responseFactory,
            IProductFactory productFactory, IFileService fileService)
        {
            _productImageRepository = productImageRepository;
            _responseFactory = responseFactory;
            _productFactory = productFactory;
            _fileService = fileService;
        }

        public Task<ResponseModel> Handle(SaveProductImagesCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
