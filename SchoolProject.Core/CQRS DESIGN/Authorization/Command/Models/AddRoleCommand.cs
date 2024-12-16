using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.CQRS_DESIGN.Authorization.Command.Models
{
    public class AddRoleCommand : IRequest<Response<string>>
    {
        public string RoleName { get; set; }
    }

}
