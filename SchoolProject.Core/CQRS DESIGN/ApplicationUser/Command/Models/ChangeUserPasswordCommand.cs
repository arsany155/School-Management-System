using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Command.Models
{
    public class ChangeUserPasswordCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
