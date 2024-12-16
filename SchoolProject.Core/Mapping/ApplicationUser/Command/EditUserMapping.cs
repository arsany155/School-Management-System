using SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Command.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
    public partial class UserProfile
    {
        public void EditUserMapping()
        {
            CreateMap<EditUserCmmand, User>();
        }
    }
}
