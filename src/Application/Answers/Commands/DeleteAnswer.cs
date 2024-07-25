using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Services.Mediator.Interfaces;
using Application.Common.Utils;
using Domain.Enums;
using Domain.Interfaces.Repositories;

namespace Application.Answers.Commands
{
    public class DeleteAnswerCommand : IRequest
    {
        public int AnswerId { get; set; }
    }

    public class DeleteAnswerCommandValidator : IRequestValidator<DeleteAnswerCommand>
    {
        public bool IsValid(DeleteAnswerCommand request)
        {
            return Validators.IsIdValid(request.AnswerId);
        }
    }

    public class DeleteAnswerCommandHandler : IRequestHandler<DeleteAnswerCommand>
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public DeleteAnswerCommandHandler(IAnswerRepository answerRepository, IUserRepository userRepository, IUserService userService)
        {
            _answerRepository = answerRepository;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task Handle(DeleteAnswerCommand request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserId();

            var user = await _userRepository.GetUserByIdAsync(authenticatedUserId);
            if (user.Role != UserRole.Admin)
            {
                throw new UnauthorizedException();
            }

            await _answerRepository.DeleteAnswer(request.AnswerId);
        }
    }
}
