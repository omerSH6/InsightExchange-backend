using Application.Answers.Commands;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using Application.Common.Services.Mediator.Interfaces;
using Application.Common.Utils;
using Domain.Enums;
using Domain.Interfaces.Repositories;

namespace Application.Questions.Queries
{
    public class GetPendingQuestionQuery : IRequest<QuestionDTO>
    {
        public int QuestionId { get; set; }
    }

    public class GetPendingQuestionQueryValidator : IRequestValidator<GetPendingQuestionQuery>
    {
        public bool IsValid(GetPendingQuestionQuery request)
        {
            return Validators.IsIdValid(request.QuestionId);
        }
    }

    public class GetPendingQuestionQueryHandler : IRequestHandler<GetPendingQuestionQuery, QuestionDTO>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public GetPendingQuestionQueryHandler(IQuestionRepository questionRepository, IUserRepository userRepository, IUserService userService)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<QuestionDTO> Handle(GetPendingQuestionQuery request)
        {
            var requestedQuestingId = request.QuestionId;
            var authenticatedUserId = _userService.GetAuthenticatedUserId();
            var question = await _questionRepository.GetByIdAsync(requestedQuestingId, QuestionState.Pending);

            var user = await _userRepository.GetUserById(authenticatedUserId);
            if (user.Role != UserRole.Admin)
            {
                throw new Exception($"User NOT authorized");
            }

            return Mapping.QuestionToQuestionDTO(question, authenticatedUserId);
        }
    }
}
