using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.Response;
using AuctionSite.Models.User.Request;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuctionSite.Application.Command
{
    public class UpdateProfilePictureCommand : UpdateProfilePictureRequest, IRequest<ResponseModel> { }

    public class UpdateProfilePictureHandler : IRequestHandler<UpdateProfilePictureCommand, ResponseModel>
    {
        private readonly IFileService _fileService;
        private readonly IResponseFactory _responseFactory;
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateProfilePictureHandler(IFileService fileService,
            IResponseFactory responseFactory,
            UserManager<ApplicationUser> userManager)
        {
            _fileService = fileService;
            _responseFactory = responseFactory;
            _userManager = userManager;
        }

        public async Task<ResponseModel> Handle(UpdateProfilePictureCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);

                if(user is null)
                {
                    return _responseFactory.CreateFailure("User not found");
                }

                var pictureName = await _fileService.SaveProfilePicture(request.File);

                _fileService.RemoveProfilePicture(user.ProfilePicture);

                user.ProfilePicture = pictureName;

                await _userManager.UpdateAsync(user);

                return _responseFactory.CreateSuccess();
            }
            catch(Exception ex) 
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }
    }
}
