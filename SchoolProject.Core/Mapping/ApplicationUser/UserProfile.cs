using AutoMapper;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
    public partial class UserProfile : Profile
    {
        public UserProfile()
        {
            AddUserMapping();
            EditUserMapping();

            GetUserPaginationMapping();
            GetUserByIdMapping();
        }
    }
}
