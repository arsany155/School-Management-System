using SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Queries.DTO;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
    public partial class UserProfile
    {
        public void GetUserPaginationMapping()
        {
            CreateMap<User, GetUserPaginationDTO>();
        }
    }
}
