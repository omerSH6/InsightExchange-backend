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
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("pagination")]
        public async Task<ActionResult<List<QuestionPreviewDTO>>> GetQuestionsWithPagination([FromQuery] GetQuestionsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<QuestionDTO>> CreateQuestion([FromBody] CreateQuestionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
