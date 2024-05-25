using Application.Services.Mediator.Interfaces;

namespace Application.Questions.Commands
{
    public class CreateQuestionCommand : IRequest
    {
    }

    public class CreateQuestionHandler : IRequestHandler<CreateQuestionCommand>
    {
        public Task Handle(CreateQuestionCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
