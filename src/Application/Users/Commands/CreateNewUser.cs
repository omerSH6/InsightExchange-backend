using Application.Common.Exceptions;
using Application.Common.Services.Mediator.Interfaces;
using Application.Common.Services.PasswordHash.Interfaces;
using Application.Common.Utils;
using Application.Questions.Commands;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;

namespace Application.Users.Commands
{
    public class CreateNewUserCommand : IRequest
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class CreateNewUserCommandValidator : IRequestValidator<CreateNewUserCommand>
    {
        public bool IsValid(CreateNewUserCommand request)
        {
            return Validators.IsShortStringValid(request.UserName) &&
                     Validators.IsShortStringValid(request.Email) &&
                     Validators.IsShortStringValid(request.Password);
        }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateNewUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;

        public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHashService passwordHashService)
        {
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
        }

        public async Task Handle(CreateNewUserCommand request)
        {
            var user = await _userRepository.GetUserByUserNameAsync(request.UserName);
            if (user != null)
            {
                throw new OperationFailedException();
            }

            var passwordHash = _passwordHashService.HashPassword(request.Password);

            var newUser = new User()
            {
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = passwordHash,
                Role = UserRole.User,
            };

            await _userRepository.AddUserAsync(newUser);
        }
    }
}
