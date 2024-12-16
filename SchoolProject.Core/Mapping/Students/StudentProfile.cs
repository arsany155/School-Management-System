using AutoMapper;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile : Profile
    {
        public StudentProfile()
        {
            //Query
            GetStudentsListMapping();
            GetStudentInfoMapping();
            GetStudentsPaginationListMapping();
            //Command
            AddStudentsMapping();
            EditStudentMapping();

        }
    }
}
