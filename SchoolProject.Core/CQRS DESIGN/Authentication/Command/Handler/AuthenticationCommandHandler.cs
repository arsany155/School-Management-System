using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.CQRS_DESIGN.Authentication.Command.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helper;
using SchoolProject.Services.Interfaces;

namespace SchoolProject.Core.CQRS_DESIGN.Authentication.Command.Handler
{
    public class AuthenticationCommandHandler : ResponseHandler, IRequestHandler<SigninCommand, Response<JwtAuthResult>>, IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>
    {
        private readonly IStringLocalizer _localization;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationServices _authenticationServices;
        public AuthenticationCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IAuthenticationServices authenticationServices, SignInManager<User> signInManager, UserManager<User> userManager) : base(stringLocalizer)
        {
            _localization = stringLocalizer;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationServices = authenticationServices;
        }

        public async Task<Response<JwtAuthResult>> Handle(SigninCommand request, CancellationToken cancellationToken)
        {
            var userName = await _userManager.FindByNameAsync(request.UserName);
            if (userName == null)
            {
                return BadRequest<JwtAuthResult>(_localization[SharedResourcesKeys.NotFound]);
            }
            var signinuser = await _signInManager.CheckPasswordSignInAsync(userName, request.Password, false);
            if (!signinuser.Succeeded)
            {
                return BadRequest<JwtAuthResult>(_localization[SharedResourcesKeys.NotFound]);
            }

            var accessToken = await _authenticationServices.GetJWTToken(userName);
            return Success(accessToken);
        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationServices.GetRefreshToken(request.AccessToken, request.RefreshToken);
            return Success(result);
        }
    }
}
