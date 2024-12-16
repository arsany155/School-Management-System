using SchoolProject.Core.CQRS_DESIGN.Departments.Queries.DTO;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmetByIdMapping()
        {
            CreateMap<Department, GetDepartmentByIdDTO>()
                .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)))
                .ForMember(dest => dest.Id, op => op.MapFrom(src => src.DID))
                .ForMember(dest => dest.ManagerName, op => op.MapFrom(src => src.Instructor.Localize(src.Instructor.NameAr, src.Instructor.NameEn)))
                .ForMember(dest => dest.Subjects, op => op.MapFrom(src => src.DepartmentSubjects))
                //.ForMember(dest => dest.Students, op => op.MapFrom(src => src.Students))
                .ForMember(dest => dest.Instructors, op => op.MapFrom(src => src.Instructors));

            CreateMap<DepartmentSubject, SubjectDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.Localize(src.Subject.SubjectNameAr, src.Subject.SubjectNameEn)));

            //CreateMap<Student, StudentDTO>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudID))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));

            CreateMap<Instructor, InstructorDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));


        }
    }
}
