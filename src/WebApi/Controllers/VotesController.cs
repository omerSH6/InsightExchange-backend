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
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("answer")]
        public async Task<IActionResult> VoteAnswer([FromBody] CreateAnswerVote command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
