using Application.Services.Mediator.Interfaces;
using Domain.Entities;

namespace Application.Users.Commands
{
    public class CreateUserCommand : IRequest
    {
    }

    public class CreateUserHandler : IRequestHandler<CreateUserCommand>
    {
        public Task Handle(CreateUserCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
