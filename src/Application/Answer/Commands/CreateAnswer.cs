using Application.Services.Mediator.Interfaces;

namespace Application.Answer.Commands
{
    public class CreateAnswerCommand : IRequest
    {
    }

    public class CreateAnswerHandler : IRequestHandler<CreateAnswerCommand>
    {
        public Task Handle(CreateAnswerCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
