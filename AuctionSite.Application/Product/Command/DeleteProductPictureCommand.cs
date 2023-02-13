using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Models.Response;
using MediatR;

namespace AuctionSite.Application.Command
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

        public async Task<ResponseModel> Handle(DeleteProductPictureCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var fileName = await _productImageRepository.GetProductImageById(request.ImageId, image => image.FileName);

                if(string.IsNullOrEmpty(fileName))
                {
                    return _responseFactory.CreateFailure("Image not found");
                }

                await _productImageRepository.DeleteProductImageByIdAsync(request.ImageId);

                _fileService.RemoveProductImage(fileName);

                return _responseFactory.CreateSuccess();
            }
            catch (Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }
    }
}
