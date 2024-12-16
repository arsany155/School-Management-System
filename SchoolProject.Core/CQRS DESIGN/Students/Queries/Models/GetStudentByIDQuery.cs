using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.CQRS_DESIGN.Students.Queries.DTO;

namespace SchoolProject.Core.CQRS_DESIGN.Students.Queries.Models
{
    public class GetStudentByIDQuery :IRequest<Response<GetStudentInfoVM>>
    {
        public int Id { get; set; }
        public GetStudentByIDQuery(int ID)
        {
            Id = ID;
        }
    }
}
