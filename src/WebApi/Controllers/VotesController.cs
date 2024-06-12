using Application.Answers.Commands;
using Application.Common.Services.Mediator.Interfaces;
using Application.Questions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : BaseApiController
    {
        public VotesController(IMediator mediator)
            : base(mediator) { }


        [HttpPost("question")]
        public async Task<IActionResult> VoteQuestion([FromBody] CreateQuestionVote command)
        {
            var result = await _mediator.Send<CreateQuestionVote, bool>(command);

            if (!result.IsSuccess)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("answer")]
        public async Task<IActionResult> VoteAnswer([FromBody] CreateAnswerVote command)
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
