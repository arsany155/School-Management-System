using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.CQRS_DESIGN.Students.Queries.DTO;
using SchoolProject.Data.Entities;


namespace SchoolProject.Core.CQRS_DESIGN.Students.Queries.Models
{
    public class GetStudentListQuery : IRequest<Response<List<GetStudentListVM>>>
    {
    }
}
