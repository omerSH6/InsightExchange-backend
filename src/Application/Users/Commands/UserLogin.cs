using Application.Common.DTOs;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Services.Mediator.Interfaces;
using Application.Common.Services.PasswordHash.Interfaces;
using Application.Common.Utils;
using Domain.Interfaces.Repositories;   


namespace Application.Users.Commands
{
    public class UserLoginCommand : IRequest<UserLoginTokenDTO>
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class UserLoginCommandValidator : IRequestValidator<UserLoginCommand>
    {
        public bool IsValid(UserLoginCommand request)
        {
            return Validators.IsShortStringValid(request.UserName) &&
                     Validators.IsShortStringValid(request.Email) &&
                     Validators.IsShortStringValid(request.Password);
        }
    }

    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, UserLoginTokenDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly ITokenProvider _tokenProvider;

        public UserLoginCommandHandler(IUserRepository userRepository, IPasswordHashService passwordHashService, ITokenProvider jwtProvider)
        {
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
            _tokenProvider = jwtProvider;
        }

        public async Task<UserLoginTokenDTO> Handle(UserLoginCommand request)
        {
            var user = await _userRepository.GetUserByUserNameAsync(request.UserName);
            if (user == null)
            {
                throw new OperationFailedException();
            }

            if(_passwordHashService.VerifyPasswordHash(request.Password, user.PasswordHash))
            {
                var UserLoginTokenDTO = new UserLoginTokenDTO()
                {
                    LoginToken = _tokenProvider.Generate(user),
                    UserName = user.UserName,
                    Role = user.Role.ToString(),
                    UserId = user.Id
                };

                return UserLoginTokenDTO;
            }

            throw new Exception("wrong password");
        }
    }
}
