using SchoolProject.Core.CQRS_DESIGN.Students.Queries.DTO;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentsPaginationListMapping()
        {
            CreateMap<Student, GetStudentPaginatedListVM>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DNameAr))
                //Localize
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))
                .ForMember(dest => dest.StudID, opt => opt.MapFrom(src => src.StudID))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
        }
    }
}

