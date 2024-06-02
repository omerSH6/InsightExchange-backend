using Application.Services.Mediator.Interfaces;
using Application.Services.Mediator;
using Domain.DTOs;

namespace Application.Users.Commands
{
    public class ValidateUserCredentialsCommand : IRequest<UserDTO>
    {
        public required string UserName { get; set; }
        public required string PasswordHash { get; set; }
    }

    public class ValidateUserCredentialsCommandHandler : IRequestHandler<ValidateUserCredentialsCommand, UserDTO>
    {
        public Task<ResultDto<UserDTO>> Handle(ValidateUserCredentialsCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
