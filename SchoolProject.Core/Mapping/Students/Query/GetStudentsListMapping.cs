using SchoolProject.Core.CQRS_DESIGN.Students.Queries.DTO;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentsListMapping()
        {
            CreateMap<Student, GetStudentListVM>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DNameAr))
                //Localize
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));
        }
    }
}
