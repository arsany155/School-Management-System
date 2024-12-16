using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Command.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Command.Handler
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<EditUserCmmand, Response<string>>,
        IRequestHandler<DeleteUserCommand, Response<string>>,
        IRequestHandler<ChangeUserPasswordCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserCommandHandler(IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer, UserManager<User> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            }

            var userName = await _userManager.FindByNameAsync(request.UserName);
            if (userName != null)
            {
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            }

            var identityUser = _mapper.Map<User>(request);

            var result = await _userManager.CreateAsync(identityUser, request.Password);

            if (!result.Succeeded)
            {
                return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            }

            return Created<string>(_stringLocalizer[SharedResourcesKeys.Created]);
        }

        public async Task<Response<string>> Handle(EditUserCmmand request, CancellationToken cancellationToken)
        {
            var olduser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (olduser == null)
            {
                return NotFound<string>();
            }
            var newUser = _mapper.Map(request, olduser);
            var update = await _userManager.UpdateAsync(newUser);
            if (!update.Succeeded) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotUpdated]);
            return Success((string)_stringLocalizer[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.id.ToString());
            if (user == null)
            {
                return NotFound<string>();
            }
            var deleteUser = await _userManager.DeleteAsync(user);
            if (!deleteUser.Succeeded) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            return Success((string)_stringLocalizer[SharedResourcesKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                return NotFound<string>();
            }

            if (!await _userManager.CheckPasswordAsync(user, request.CurrentPassword))
            {
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotUpdated]);
            }

            await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            return Success((string)_stringLocalizer[SharedResourcesKeys.Updated]);
        }
    }
}
