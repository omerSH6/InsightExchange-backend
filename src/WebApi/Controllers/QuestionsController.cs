using Application.Answers.Commands;
using Application.Questions.Commands;
using Application.Questions.Queries;
using Application.Services.Mediator.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : BaseApiController
    {
        public QuestionsController(IMediator mediator)
            : base(mediator) { }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetQuestionByIdAsync([FromQuery] GetQuestionQuery query)
        {
           
            var result = await _mediator.SendAsync(query);

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            return Ok(result.Data);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetQuestionsAsync([FromQuery] GetQuestionsQuery query)
        {
            var result = await _mediator.SendAsync(query);

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            return Ok(result.Data);
        }

        [HttpPost("question")]
        public async Task<IActionResult> Question([FromBody] CreateQuestionCommand command)
        {
            var result = await _mediator.Send<CreateQuestionCommand, bool>(command);

            if (!result.IsSuccess)
            {
                return BadRequest();
            }

            return Ok(result.Data);
        }

        [HttpPost("vote")]
        public async Task<IActionResult> Vote([FromBody] CreateQuestionVote command)
        {
            var result = await _mediator.Send<CreateQuestionVote, bool>(command);

            if (!result.IsSuccess)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
