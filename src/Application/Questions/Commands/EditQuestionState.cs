using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Services.Mediator.Interfaces;
using Application.Common.Utils;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;

namespace Application.Questions.Commands
{
    public class EditQuestionStateCommand : IRequest
    {
        public int QuestionId { get; set; }
        public QuestionState QuestionState { get; set; }
    }

    public class EditQuestionStateCommandValidator : IRequestValidator<EditQuestionStateCommand>
    {
        public bool IsValid(EditQuestionStateCommand request)
        {
            return Validators.IsIdValid(request.QuestionId);
        }
    }

    public class EditQuestionStateCommandHandler : IRequestHandler<EditQuestionStateCommand>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public EditQuestionStateCommandHandler(IQuestionRepository questionRepository, IUserRepository userRepository, IUserService userService)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task Handle(EditQuestionStateCommand request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserId();
            var question = await _questionRepository.GetByIdAsync(request.QuestionId);
            var originalQuestionState = question.State;
            var user = await _userRepository.GetUserByIdAsync(authenticatedUserId);
            if (user.Role != UserRole.Admin)
            {
                throw new UnauthorizedException();
            }

            await _questionRepository.EditQuestionState(request.QuestionId, request.QuestionState);

            if (question.State != originalQuestionState)
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
                    UserId = question.UserId
                };
                await _userRepository.AddUserNotification(question.UserId, notification);
            }
        }
    }
}
