using Microsoft.AspNetCore.Mvc;
using Application.Services.Mediator.Interfaces;
using Application.Users.Commands;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        public AccountController(IMediator mediator)
            : base(mediator) { }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateNewUserCommand command)
        {
            var result = await _mediator.SendAsync<CreateNewUserCommand>(command);
            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(new { Message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginTokenDTO>> Login([FromBody] UserLoginCommand command)
        {
            var result = await _mediator.Send<UserLoginCommand, UserLoginTokenDTO>(command);

            if (!result.IsSuccess)
            {
                return Unauthorized();
            }

            return Ok(result.Data);
        }
    }
}
