using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using MediatR;

namespace AuctionSite.Application.Command
{
    public class GetProductImageQuery : IRequest<FileStream>
    {
        public int ImageId { get; set; }
    }

    public class GetProductImageHandler : IRequestHandler<GetProductImageQuery, FileStream>
    {
        private readonly IFileService _fileService;
        private readonly IProductImageRepository _productImageRepository;

        public GetProductImageHandler(IFileService fileService, IProductImageRepository productImageRepository)
        {
            _fileService = fileService;
            _productImageRepository = productImageRepository;
        }

        public Task<FileStream> Handle(GetProductImageQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
