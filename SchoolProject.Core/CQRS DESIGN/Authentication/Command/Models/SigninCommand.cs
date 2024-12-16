using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Helper;

namespace SchoolProject.Core.CQRS_DESIGN.Authentication.Command.Models
{
    public class SigninCommand : IRequest<Response<JwtAuthResult>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
