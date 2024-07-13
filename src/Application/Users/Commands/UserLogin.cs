using Application.Common.DTOs;
using Application.Common.Interfaces;
using Application.Common.Services.Mediator.Interfaces;
using Application.Common.Services.PasswordHash.Interfaces;
using Domain.Interfaces.Repositories;   


namespace Application.Users.Commands
{
    public class UserLoginCommand : IRequest<UserLoginTokenDTO>
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, UserLoginTokenDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly ITokenProvider _jwtProvider;

        public UserLoginCommandHandler(IUserRepository userRepository, IPasswordHashService passwordHashService, ITokenProvider jwtProvider)
        {
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
            _jwtProvider = jwtProvider;
        }

        public async Task<UserLoginTokenDTO> Handle(UserLoginCommand request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("user not exist");
            }

            if(_passwordHashService.VerifyPasswordHash(request.Password, user.PasswordHash))
            {
                var UserLoginTokenDTO = new UserLoginTokenDTO()
                {
                    LoginToken = _jwtProvider.Generate(user),
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
