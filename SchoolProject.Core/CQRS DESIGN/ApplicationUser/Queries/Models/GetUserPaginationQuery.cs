using MediatR;
using SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Queries.DTO;
using SchoolProject.Core.Wrapper;

namespace SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Queries.Models
{
    public class GetUserPaginationQuery : IRequest<PaginationResult<GetUserPaginationDTO>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
