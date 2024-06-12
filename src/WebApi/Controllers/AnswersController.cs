using Application.Answers.Commands;
using Application.Common.DTOs;
using Application.Common.Services.Mediator.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
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
    }
}
