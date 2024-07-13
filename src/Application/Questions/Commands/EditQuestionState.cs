using Application.Common.Services.Mediator.Interfaces;
using Domain.Entities;
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
        private readonly IUserService _userService;

        public EditQuestionStateHandler(IQuestionRepository questionRepository, IUserRepository userRepository, IUserService userService)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task Handle(EditQuestionStateCommand request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserId();
            var question = await _questionRepository.GetByIdAsync(request.QuestionId);
            var user = await _userRepository.GetUserById(authenticatedUserId);
            if (user.Role != UserRole.Admin)
            {
                throw new Exception($"User NOT authorized");
            }

            await _questionRepository.EditQuestionState(request.QuestionId, request.QuestionState);

            if (question.State != request.QuestionState)
            {
                string questionState = "";
                switch (request.QuestionState)
                {
                    case QuestionState.Approved:
                        questionState = "approved";
                        break;
                    case QuestionState.NotApproved:
                        questionState = "not approved";
                        break;
                    case QuestionState.Pending:
                        questionState = "moved to seccond considiration";
                        break;
                }
                var notificationMessage = $"Your question {question.Title} was {questionState} by the system administrator";
                var notification = new Notification() 
                { 
                    Content = notificationMessage,
                    CreatedAt = DateTime.Now,
                    UserId = authenticatedUserId
                };
                await _userRepository.AddUserNotification(authenticatedUserId, notification);
            }
        }
    }
}
