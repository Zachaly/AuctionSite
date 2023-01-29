using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuctionSite.Application.Command
{
    public class GetProfilePictureQuery : IRequest<FileStream>
    {
        public string UserId { get; set; }
    }

    public class GetProfilePictureHandler : IRequestHandler<GetProfilePictureQuery, FileStream>
    {
        private readonly IFileService _fileService;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetProfilePictureHandler(IFileService fileService, UserManager<ApplicationUser> userManager)
        {
            _fileService = fileService;
            _userManager = userManager;
        }

        public async Task<FileStream> Handle(GetProfilePictureQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);



            return _fileService.GetProfilePicture(user?.ProfilePicture ?? "");
        }
    }
}
