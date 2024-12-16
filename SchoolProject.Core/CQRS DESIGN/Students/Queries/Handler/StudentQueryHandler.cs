using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.CQRS_DESIGN.Students.Queries.DTO;
using SchoolProject.Core.CQRS_DESIGN.Students.Queries.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Core.Wrapper;
using SchoolProject.Services.IServices;

namespace SchoolProject.Core.CQRS_DESIGN.Students.Queries.Handler
{
    public class StudentQueryHandler : ResponseHandler,
                                        IRequestHandler<GetStudentListQuery, Response<List<GetStudentListVM>>>,
                                        IRequestHandler<GetStudentByIDQuery, Response<GetStudentInfoVM>>,
                                        IRequestHandler<GetStudentPaginatedListQuery, PaginationResult<GetStudentPaginatedListVM>>

    {
        private readonly IStudentServices _studentServices;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public StudentQueryHandler(IStudentServices studentServices, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentServices = studentServices;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<List<GetStudentListVM>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var studentList = await _studentServices.GetAllStudentsAsync();
            var studentListMapper = _mapper.Map<List<GetStudentListVM>>(studentList);
            var result = Success(studentListMapper);
            result.Meta = new { Count = studentListMapper.Count() };
            return result;
        }

        public async Task<Response<GetStudentInfoVM>> Handle(GetStudentByIDQuery request, CancellationToken cancellationToken)
        {
            var studentInfo = await _studentServices.GetStudentByIdAsync(request.Id);
            if (studentInfo == null) return NotFound<GetStudentInfoVM>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var studentInfoMapper = _mapper.Map<GetStudentInfoVM>(studentInfo);
            return Success(studentInfoMapper);
        }

        public async Task<PaginationResult<GetStudentPaginatedListVM>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            // Expression<Func<Student, GetStudentPaginatedListVM>> expression = e => new GetStudentPaginatedListVM(e.StudID, e.Localize(e.NameAr, e.NameEn), e.Address, e.Localize(e.Department.DNameAr, e.Department.DNameEn));
            var FilterQuery = _studentServices.FilterStudentPaginatedQuery(request.OrderBy, request.Search);
            var paginationList = await _mapper.ProjectTo<GetStudentPaginatedListVM>(FilterQuery, null).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginationList;
        }
    }
}
