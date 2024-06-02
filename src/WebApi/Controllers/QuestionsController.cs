using Application.Questions.Queries;
using Application.Services.Mediator.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetQuestionByIdAsync(int id)
        {
            var getQuestionQuery = new GetQuestionQuery() 
            {
                Id = id
            };

            var result = await _mediator.SendAsync(getQuestionQuery);

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            return Ok(result.Data);
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetQuestionByIdAsync(int id)
        {
            var getQuestionQuery = new GetQuestionQuery()
            {
                Id = id
            };

            var result = await _mediator.SendAsync(getQuestionQuery);

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            return Ok(result.Data);
        }


    }
}
