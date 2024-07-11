using Application.Common.Services.Mediator.Interfaces;
using Domain.Enums;
using Domain.Interfaces.Authentication;
using Domain.Interfaces.Repositories;

namespace Application.Questions.Commands
{
    public class EditQuestionStateCommand : IRequest
    {
        public int QuestionId { get; set; }
        public QuestionState QuestionState { get; set; }
    }

    public class EditQuestionStateHandler : IRequestHandler<EditQuestionStateCommand>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IUserService _userService;

        public EditQuestionStateHandler(IQuestionRepository questionRepository, IUserRepository userRepository, ITagRepository tagRepository, IUserService userService)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _tagRepository = tagRepository;
            _userService = userService;
        }

        public async Task Handle(EditQuestionStateCommand request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserId();

            var user = await _userRepository.GetUserById(authenticatedUserId);
            if (user.Role != UserRole.Admin)
            {
                throw new Exception($"User NOT authorized");
            }

            await _questionRepository.EditQuestionState(request.QuestionId, request.QuestionState);
        }
    }
}
