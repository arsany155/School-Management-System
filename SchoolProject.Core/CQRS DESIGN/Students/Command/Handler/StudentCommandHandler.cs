using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.CQRS_DESIGN.Students.Command.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Data.Entities;
using SchoolProject.Services.IServices;

namespace SchoolProject.Core.CQRS_DESIGN.Students.Command.Handler
{
    public class StudentCommandHandler : ResponseHandler,
                                        IRequestHandler<AddStudentCommand, Response<string>>,
                                        IRequestHandler<EditStudentCommand, Response<string>>,
                                        IRequestHandler<DeleteStudentCommand, Response<string>>

    {
        private readonly IStudentServices _studentServices;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;


        public StudentCommandHandler(IStudentServices studentServices, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentServices = studentServices;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentmapper = _mapper.Map<Student>(request);
            var result = await _studentServices.AddStudent(studentmapper);
            if (result == "Success") return Created("");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var user = await _studentServices.GetStudentByIdAsync(request.Id);
            if (user == null) return NotFound<string>("");
            var studentmapper = _mapper.Map(request, user);

            var result = await _studentServices.UpdateStudent(studentmapper);
            if (result.Equals("Success")) return Success("Edit Successfully");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var user = await _studentServices.GetStudentByIdWithoutDepartmentAsync(request.Id);
            if (user == null) return NotFound<string>("Student not found");
            var result = await _studentServices.DeleteStudent(user);
            if (result.Equals("Success")) return Deleted<string>("");
            else return BadRequest<string>();
        }
    }
}
