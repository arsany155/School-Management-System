using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.CQRS_DESIGN.Authorization.Command.Models;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : AppControllerBase
    {
        [HttpPost("/addRole")]
        public async Task<IActionResult> AddnewRole([FromForm] AddRoleCommand addRole)
        {
            var response = await Mediator.Send(addRole);
            return NewResult(response);
        }
    }
}
