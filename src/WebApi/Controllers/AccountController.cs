using Microsoft.AspNetCore.Mvc;
using Application.Services.Mediator.Interfaces;
using Application.Users.Commands;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        public AccountController(IMediator mediator)
            : base(mediator) { }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateNewUserCommand command)
        {
            var result = await _mediator.Send<CreateNewUserCommand, bool>(command);
            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(new { Message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginCommand command)
        {
            var result = await _mediator.Send<UserLoginCommand, UserLoginTokenDTO>(command);

            if (!result.IsSuccess)
            {
                return Unauthorized();
            }

            var token = result.Data.LoginToken;

            return Ok(new { Token = token });
        }
    }
}
