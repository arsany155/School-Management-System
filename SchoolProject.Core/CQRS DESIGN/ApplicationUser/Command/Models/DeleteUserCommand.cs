using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Command.Models
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public int id { get; set; }

        public DeleteUserCommand(int Id)
        {
            id = Id;
        }
    }
}
