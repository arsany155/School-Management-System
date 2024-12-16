using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.CQRS_DESIGN.Authorization.Command.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Services.Interfaces;

namespace SchoolProject.Core.CQRS_DESIGN.Authorization.Command.Handler
{
    public class RoleCommandHandler : ResponseHandler, IRequestHandler<AddRoleCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _StringLocalizer;
        private readonly IAuthorizationRoleServices _authorizationRoleServices;
        public RoleCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IAuthorizationRoleServices authorizationRoleServices) : base(stringLocalizer)
        {
            _StringLocalizer = stringLocalizer;
            _authorizationRoleServices = authorizationRoleServices;
        }

        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationRoleServices.IsRoleExist(request.RoleName);

            if (result == false)
            {
                return BadRequest<string>("The Role Name was Found In Database");
            }

            var newRole = await _authorizationRoleServices.AddRole(request.RoleName);
            if (newRole == "Failed")
            {
                return BadRequest<string>("The Role Name doesn't want to save in database");
            }

            return Success<string>("The new Role Name saved in Database");
        }
    }
}
