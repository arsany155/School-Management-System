
using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Command.Models
{
    public class EditUserCmmand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
