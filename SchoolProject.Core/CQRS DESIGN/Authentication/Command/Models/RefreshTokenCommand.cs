using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Helper;

namespace SchoolProject.Core.CQRS_DESIGN.Authentication.Command.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthResult>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
