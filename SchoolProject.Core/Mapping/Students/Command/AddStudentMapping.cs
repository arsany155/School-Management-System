﻿using SchoolProject.Core.CQRS_DESIGN.Students.Command.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void AddStudentsMapping()
        {
            CreateMap<AddStudentCommand, Student>()
                .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.NameAr))
                .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn));
        }
    }
}