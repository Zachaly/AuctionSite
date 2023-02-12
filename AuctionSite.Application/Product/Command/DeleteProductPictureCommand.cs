using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Models.Response;
using MediatR;

namespace AuctionSite.Application.Product.Command
{
    public class DeleteProductPictureCommand : IRequest<ResponseModel>
    {
        public int ImageId { get; set; }
    }

    public class DeleteProductPictureHandler : IRequestHandler<DeleteProductPictureCommand, ResponseModel>
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IFileService _fileService;
        private readonly IResponseFactory _responseFactory;

        public DeleteProductPictureHandler(IProductImageRepository productImageRepository,
            IFileService fileService,
            IResponseFactory responseFactory)
        {
            _productImageRepository = productImageRepository;
            _fileService = fileService;
            _responseFactory = responseFactory;
        }

        public Task<ResponseModel> Handle(DeleteProductPictureCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
