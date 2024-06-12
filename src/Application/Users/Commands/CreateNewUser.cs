using Application.Common.Services.Mediator.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Authentication;
using Domain.Interfaces.Repositories;
using Domain.Shared;

namespace Application.Users.Commands
{
    public class CreateNewUserCommand : IRequest
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class CreateUserHandler : IRequestHandler<CreateNewUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;

        public CreateUserHandler(IUserRepository userRepository, IPasswordHashService passwordHashService)
        {
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
        }

        public async Task<ResultDto<bool>> Handle(CreateNewUserCommand request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user != null)
            {
                return ResultDto<bool>.Fail("user with this email already exist");
            }
            
            user = await _userRepository.GetUserByUserNameAsync(request.UserName);
            if (user != null)
            {
                return ResultDto<bool>.Fail("user with this user name already exist");
            }

            var passwordHash = _passwordHashService.HashPassword(request.Password);

            var newUser = new User()
            {
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = passwordHash,
            };

            await _userRepository.AddUserAsync(newUser);

            return ResultDto<bool>.Success(true);
        }
    }
}
