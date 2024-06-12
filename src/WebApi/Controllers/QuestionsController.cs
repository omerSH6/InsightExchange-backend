using Application.Answers.Commands;
using Application.Common.DTOs;
using Application.Common.Services.Mediator.Interfaces;
using Application.Questions.Commands;
using Application.Questions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    public class QuestionsController : BaseApiController
    {
        public QuestionsController(IMediator mediator)
            : base(mediator) { }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<QuestionDTO>> GetQuestion([FromQuery] GetQuestionQuery query)
        {
           
            var result = await _mediator.Send<GetQuestionQuery, QuestionDTO>(query);

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            return Ok(result.Data);
        }

        [AllowAnonymous]
        [HttpGet("pagination")]
        public async Task<ActionResult<List<QuestionPreviewDTO>>> GetQuestionsWithPagination([FromQuery] GetQuestionsQuery query)
        {
            var result = await _mediator.Send<GetQuestionsQuery, List<QuestionPreviewDTO>>(query);

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<ActionResult<QuestionDTO>> CreateQuestion([FromBody] CreateQuestionCommand command)
        {
            var result = await _mediator.Send<CreateQuestionCommand, QuestionDTO>(command);

            if (!result.IsSuccess)
            {
                return BadRequest();
            }

            return Ok(result.Data);
        }
    }
}
