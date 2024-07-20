using Application.Common.DTOs;
using Application.Common.Interfaces;
using Application.Common.Services.Mediator.Interfaces;
using Application.Common.Utils;
using Domain.Enums;
using Domain.Interfaces.Repositories;

namespace Application.Questions.Queries
{
    public class GetQuestionQuery : IRequest<QuestionDTO>
    {
        public int QuestionId { get; set; }
    }

    public class GetQuestionQueryValidator : IRequestValidator<GetQuestionQuery>
    {
        public bool IsValid(GetQuestionQuery request)
        {
            return Validators.IsIdValid(request.QuestionId);
        }
    }

    public class GetQuestionQueryHandler : IRequestHandler<GetQuestionQuery, QuestionDTO>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public GetQuestionQueryHandler(IQuestionRepository questionRepository, IUserRepository userRepository, IUserService userService)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<QuestionDTO> Handle(GetQuestionQuery request)
        {
            var requestedQuestingId = request.QuestionId;
            var authenticatedUserId = _userService.GetAuthenticatedUserIfExist();
            var question = await _questionRepository.GetByIdAsync(requestedQuestingId, QuestionState.Approved);

            if (question == null)
            {
                throw new Exception($"The question with the id of {requestedQuestingId} not exist");
            }

            return Mapping.QuestionToQuestionDTO(question, authenticatedUserId);
        }
    }
}
