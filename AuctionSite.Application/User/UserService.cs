using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Response;
using AuctionSite.Models.User;
using AuctionSite.Models.User.Request;
using AuctionSite.Models.User.Validator;
using Microsoft.AspNetCore.Identity;

namespace AuctionSite.Application.User
{
    [Implementation(typeof(IUserService))]
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IResponseFactory _responseFactory;
        private readonly IUserFactory _userFactory;

        public UserService(UserManager<ApplicationUser> userManager, IUserInfoRepository userInfoRepository,
            IResponseFactory responseFactory, IUserFactory userFactory)
        {
            _userManager = userManager;
            _userInfoRepository = userInfoRepository;
            _responseFactory = responseFactory;
            _userFactory = userFactory;
        }

        public async Task<DataResponseModel<UserProfileModel>> GetUserByIdAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return _responseFactory.CreateFailure<UserProfileModel>("User not found");
                }

                user.Info = await _userInfoRepository.GetUserInfoByIdAsync(id, x => x);
                return _responseFactory.CreateSuccess(_userFactory.CreateProfileModel(user));
            }
            catch(Exception ex)
            {
                return _responseFactory.CreateFailure<UserProfileModel>(ex.Message);
            }
        }

        public async Task<ResponseModel> UpdateUserAsync(UpdateUserRequest request)
        {
            var validation = new UpdateUserRequestValidator().Validate(request);

            if(!validation.IsValid) 
            {
                return _responseFactory.CreateValidationError(validation);
            }

            try
            {
                var user = await _userManager.FindByIdAsync(request.Id);

                if(user == null)
                {
                    return _responseFactory.CreateFailure("User not found");
                }

                user.Info = await _userInfoRepository.GetUserInfoByIdAsync(request.Id, x => x);

                user.UserName = request.UserName ?? user.UserName;
                user.Info.Address = request.Address ?? user.Info.Address;
                user.Info.FirstName = request.FirstName ?? user.Info.FirstName;
                user.Info.LastName = request.LastName ?? user.Info.LastName ;
                user.Info.PhoneNumber = request.PhoneNumber ?? user.Info.PhoneNumber;
                user.Info.City = request.City ?? user.Info.City;
                user.Info.Country = request.Country ?? user.Info.Country;
                user.Info.Gender = request.Gender ?? user.Info.Gender;
                user.Info.PostalCode = request.PostalCode ?? user.Info.PostalCode;

                var res = await _userManager.UpdateAsync(user);

                return res.Succeeded ? _responseFactory.CreateSuccess() : _responseFactory.CreateFailure("Error occured");
            }
            catch (Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }
    }
}
