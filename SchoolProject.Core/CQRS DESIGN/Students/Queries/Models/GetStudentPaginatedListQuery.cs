using MediatR;
using SchoolProject.Core.CQRS_DESIGN.Students.Queries.DTO;
using SchoolProject.Core.Wrapper;
using SchoolProject.Data.Helper;

namespace SchoolProject.Core.CQRS_DESIGN.Students.Queries.Models
{
    public class GetStudentPaginatedListQuery : IRequest<PaginationResult<GetStudentPaginatedListVM>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public StudentOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
