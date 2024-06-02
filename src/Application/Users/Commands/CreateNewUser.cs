using Application.Services.Mediator;
using Application.Services.Mediator.Interfaces;
using Domain.Entities;

namespace Application.Users.Commands
{
    public class CreateNewUserCommand : IRequest
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
    }

    public class CreateUserHandler : IRequestHandler<CreateNewUserCommand>
    {
        public Task<ResultDto<bool>> Handle(CreateNewUserCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
