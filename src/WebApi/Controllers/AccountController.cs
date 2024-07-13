using Microsoft.AspNetCore.Mvc;
using Application.Users.Commands;
using Microsoft.AspNetCore.Authorization;
using Application.Common.DTOs;
using Application.Common.Services.Mediator.Interfaces;
using Application.Users.Queries;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    public class AccountController : BaseApiController
    {
        public AccountController(IMediator mediator)
            : base(mediator) { }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateNewUserCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { Message = "User registered successfully" });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginTokenDTO>> Login([FromBody] UserLoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("notifications")]
        public async Task<ActionResult<List<UserNotificationDTO>>> GetUserNotifications([FromQuery] GetUserNotificationsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
