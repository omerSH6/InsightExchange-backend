using Application.Answers.Commands;
using Application.DTOs;
using Application.Services.Mediator.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AnswersController : BaseApiController
    {
        public AnswersController(IMediator mediator)
            : base(mediator) { }

        [HttpPost]
        public async Task<ActionResult<AnswerDTO>> Answer([FromBody] CreateAnswerCommand command)
        {
            var result = await _mediator.Send<CreateAnswerCommand, AnswerDTO>(command);

            if (!result.IsSuccess)
            {
                return BadRequest();
            }

            return Ok(result.Data);
        }

        [HttpPost("vote")]
        public async Task<IActionResult> Vote([FromBody] CreateAnswerVote command)
        {
            var result = await _mediator.Send<CreateAnswerVote, bool>(command);

            if (!result.IsSuccess)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
