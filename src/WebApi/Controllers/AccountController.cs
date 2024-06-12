using Microsoft.AspNetCore.Mvc;
using Application.Users.Commands;
using Microsoft.AspNetCore.Authorization;
using Application.Common.DTOs;
using Application.Common.Services.Mediator.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    public class AccountController : BaseApiController
    {
        public AccountController(IMediator mediator)
            : base(mediator) { }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateNewUserCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { Message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginTokenDTO>> Login([FromBody] UserLoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
