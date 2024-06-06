using Application.Answers.Commands;
using Application.Services.Mediator.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswersController : BaseApiController
    {
        public AnswersController(IMediator mediator)
            : base(mediator) { }

        [HttpPost("answer")]
        public async Task<IActionResult> Answer([FromBody] CreateAnswerCommand command)
        {
            var result = await _mediator.Send<CreateAnswerCommand, bool>(command);

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
