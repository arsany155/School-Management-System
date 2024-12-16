using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.CQRS_DESIGN.Departments.Queries.Models;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : AppControllerBase
    {
        [HttpGet("/Student/{id}")]
        public async Task<IActionResult> GetStudentById([FromQuery] GetDepartmentByIdQuery query)
        {
            return NewResult(await Mediator.Send(query));
        }
    }
}
