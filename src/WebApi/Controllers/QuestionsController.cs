using Application.Answers.Commands;
using Application.DTOs;
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
        public async Task<ActionResult<QuestionDTO>> GetQuestionByIdAsync([FromQuery] GetQuestionQuery query)
        {
           
            var result = await _mediator.Send<GetQuestionQuery, QuestionDTO>(query);

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            return Ok(result.Data);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<QuestionPreviewDTO>>> GetQuestionsAsync([FromQuery] GetQuestionsQuery query)
        {
            var result = await _mediator.Send<GetQuestionsQuery, List<QuestionPreviewDTO>>(query);

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            return Ok(result.Data);
        }

        [HttpPost("question")]
        public async Task<ActionResult<QuestionDTO>> Question([FromBody] CreateQuestionCommand command)
        {
            var result = await _mediator.Send<CreateQuestionCommand, QuestionDTO>(command);

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
