using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.CQRS_DESIGN.Authentication.Command.Models;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost("/Login")]
        public async Task<IActionResult> Login([FromForm] SigninCommand request)
        {
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        [HttpPost("/RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand request)
        {
            var response = await Mediator.Send(request);
            return NewResult(response);
        }
    }
}
