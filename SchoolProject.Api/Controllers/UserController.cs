using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Command.Models;
using SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Queries.Models;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : AppControllerBase
    {
        [HttpPost("/User")]
        public async Task<IActionResult> Register([FromBody] AddUserCommand request)
        {
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        [HttpGet("/UserList")]
        public async Task<IActionResult> GetAllUsers([FromRoute] int id)
        {
            var response = await Mediator.Send(new GetUserByIdQuery(id));
            return Ok(response);
        }

        [HttpGet("/UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] EditUserCmmand request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("/DeleteUser")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id)
        {
            var response = await Mediator.Send(new DeleteUserCommand(id));
            return Ok(response);
        }

        [HttpGet("/ChangeUserPassword")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangeUserPasswordCommand request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }


    }
}
