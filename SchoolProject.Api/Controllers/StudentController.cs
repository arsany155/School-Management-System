using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.CQRS_DESIGN.Students.Command.Models;
using SchoolProject.Core.CQRS_DESIGN.Students.Queries.Models;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : AppControllerBase
    {
        //private readonly IMediator _mediator;

        //public StudentController(IMediator mediator) 
        //{
        //    _mediator = mediator;
        //}


        [HttpGet("/Students/List")]
        public async Task<IActionResult> GetAllStudents()
        {
            var response = await Mediator.Send(new GetStudentListQuery());
            return Ok(response);
        }


        [HttpGet("/Students/List/ByPagination")]
        public async Task<IActionResult> GetAllStudentsPaginated([FromQuery] GetStudentPaginatedListQuery request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }


        [HttpGet("/Student/{id}")]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            return NewResult(await Mediator.Send(new GetStudentByIDQuery(id)));
        }


        [HttpPost("/Add/Student")]
        public async Task<IActionResult> AddStudentById([FromBody] AddStudentCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }


        [HttpPut("/Edit/Student")]
        public async Task<IActionResult> EditStudentById([FromBody] EditStudentCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }


        [HttpDelete("/Delete/Student")]
        public async Task<IActionResult> DeleteStudentById([FromQuery] int id)
        {
            var response = await Mediator.Send(new DeleteStudentCommand(id));
            return NewResult(response);
        }
    }
}
