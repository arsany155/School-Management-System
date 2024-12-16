using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.CQRS_DESIGN.Departments.Queries.DTO;
using SchoolProject.Core.CQRS_DESIGN.Departments.Queries.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Core.Wrapper;
using SchoolProject.Data.Entities;
using SchoolProject.Services.Interfaces;
using SchoolProject.Services.IServices;
using System.Linq.Expressions;

namespace SchoolProject.Core.CQRS_DESIGN.Departments.Queries.Handler
{
    public class DepartmentQueryHandler : ResponseHandler, IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdDTO>>
    {
        private readonly IDepartmentServices _departmentServices;
        private readonly IStudentServices _studentServices;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public DepartmentQueryHandler(IDepartmentServices departmentServices, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer, IStudentServices studentServices) : base(stringLocalizer)
        {
            _departmentServices = departmentServices;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _studentServices = studentServices;
        }

        public async Task<Response<GetDepartmentByIdDTO>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var reponse = await _departmentServices.GetDepartmentByIdAsync(request.Id);
            if (reponse == null) return NotFound<GetDepartmentByIdDTO>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var mapper = _mapper.Map<GetDepartmentByIdDTO>(reponse);
            Expression<Func<Student, StudentDTO>> expression = e => new StudentDTO(e.StudID, e.Localize(e.NameAr, e.NameEn));
            var result = _studentServices.GetAllStudentsByDepartmentId(request.Id);
            var paginationList = await result.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            mapper.Students = paginationList;
            return Success(mapper);
        }
    }
}
