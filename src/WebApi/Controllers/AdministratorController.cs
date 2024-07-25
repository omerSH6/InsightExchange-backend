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

        [HttpGet("getPendingQuestion")]
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
        public async Task<ActionResult> EditQuestionState([FromBody] EditQuestionStateCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("deleteQuestion")]
        public async Task<ActionResult> DeleteQuestion([FromBody] DeleteQuestionCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        
        [HttpPost("deleteAnswer")]
        public async Task<ActionResult> DeleteAnswer([FromBody] DeleteAnswerCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
