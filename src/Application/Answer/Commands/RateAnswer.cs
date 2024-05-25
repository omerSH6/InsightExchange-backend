using Application.Services.Mediator.Interfaces;

namespace Application.Answer.Commands
{
    public class RateAnswerCommand : IRequest
    {
    }

    public class RateAnswerHandler : IRequestHandler<RateAnswerCommand>
    {
        public Task Handle(RateAnswerCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
