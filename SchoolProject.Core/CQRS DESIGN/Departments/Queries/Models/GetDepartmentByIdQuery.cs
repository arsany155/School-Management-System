using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.CQRS_DESIGN.Departments.Queries.DTO;

namespace SchoolProject.Core.CQRS_DESIGN.Departments.Queries.Models
{
    public class GetDepartmentByIdQuery : IRequest<Response<GetDepartmentByIdDTO>>
    {
        public int Id { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
