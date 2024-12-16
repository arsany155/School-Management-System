
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Queries.DTO;

namespace SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Queries.Models
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdDTO>>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
