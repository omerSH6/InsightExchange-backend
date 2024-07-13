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
    public class AdministratorController : BaseApiController
    {
        public AdministratorController(IMediator mediator)
            : base(mediator) { }

        [HttpGet]
        public async Task<ActionResult<QuestionDTO>> GetPendingQuestion([FromQuery] GetPendingQuestionQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("getPendingQuestionsWithPagination")]
        public async Task<ActionResult<List<QuestionPreviewDTO>>> GetPendingQuestionsWithPagination([FromQuery] GetPendingQuestionsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        
        [HttpPut("editQuestionState")]
        public async Task<ActionResult> EditQuestionState([FromQuery] EditQuestionStateCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("deleteQuestion")]
        public async Task<ActionResult<List<QuestionPreviewDTO>>> DeleteQuestion([FromQuery] DeleteQuestionCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        
        [HttpGet("deleteAnswer")]
        public async Task<ActionResult<List<QuestionPreviewDTO>>> DeleteAnswer([FromQuery] DeleteAnswerCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
