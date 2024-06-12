using Application.Common.DTOs;
using Application.Common.Services.Mediator.Interfaces;
using Domain.Interfaces.Authentication;
using Domain.Interfaces.Repositories;
using Domain.Shared;


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
        private readonly IJwtProvider _jwtProvider;

        public UserLoginCommandHandler(IUserRepository userRepository, IPasswordHashService passwordHashService, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
            _jwtProvider = jwtProvider;
        }

        public async Task<ResultDto<UserLoginTokenDTO>> Handle(UserLoginCommand request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return ResultDto<UserLoginTokenDTO>.Fail("user not exist");
            }

            if(_passwordHashService.VerifyPasswordHash(request.Password, user.PasswordHash))
            {
                var UserLoginTokenDTO = new UserLoginTokenDTO() 
                {
                    LoginToken = _jwtProvider.Generate(user),
                };

                return ResultDto<UserLoginTokenDTO>.Success(UserLoginTokenDTO);
            }

            return ResultDto<UserLoginTokenDTO>.Fail("wrong password");
        }
    }
}
