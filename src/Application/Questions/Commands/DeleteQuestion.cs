using Application.Common.Interfaces;
using Application.Common.Services.Mediator.Interfaces;
using Application.Common.Utils;
using Domain.Enums;
using Domain.Interfaces.Repositories;

namespace Application.Questions.Commands
{
    public class DeleteQuestionCommand : IRequest
    {
        public int QuestionId { get; set; }
    }

    public class DeleteQuestionCommandValidator : IRequestValidator<DeleteQuestionCommand>
    {
        public bool IsValid(DeleteQuestionCommand request)
        {
            return Validators.IsIdValid(request.QuestionId);
        }
    }

    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public DeleteQuestionCommandHandler(IQuestionRepository questionRepository, IUserRepository userRepository, IUserService userService)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task Handle(DeleteQuestionCommand request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserId();

            var user = await _userRepository.GetUserByIdAsync(authenticatedUserId);
            if (user.Role != UserRole.Admin)
            {
                throw new Exception($"User NOT authorized");
            }

            await _questionRepository.DeleteQuestion(request.QuestionId);
        }
    }
}
