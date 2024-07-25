using Application.Common.DTOs;
using Application.Common.Interfaces;
using Application.Common.Services.Mediator.Interfaces;
using Application.Common.Utils;
using Domain.Enums;
using Domain.Interfaces.Repositories;

namespace Application.Questions.Queries
{
    public class GetPendingQuestionsQuery : IRequest<List<QuestionPreviewDTO>>
    {
        public string? Tag { get; set; }
        public required SortBy SortBy { get; set; }
        public required SortDirection SortDirection { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class GetPendingQuestionsQueryValidator : IRequestValidator<GetPendingQuestionsQuery>
    {
        public bool IsValid(GetPendingQuestionsQuery request)
        {
            return Validators.IsIdValid(request.Page) && Validators.IsIdValid(request.PageSize);
        }
    }

    public class GetPendingQuestionsQueryHandler : IRequestHandler<GetPendingQuestionsQuery, List<QuestionPreviewDTO>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public GetPendingQuestionsQueryHandler(IQuestionRepository questionRepository, IUserRepository userRepository, IUserService userService)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<List<QuestionPreviewDTO>> Handle(GetPendingQuestionsQuery request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserId();

            var user = await _userRepository.GetUserByIdAsync(authenticatedUserId);
            if (user.Role != UserRole.Admin)
            {
                throw new Exception($"User NOT authorized");
            }

            var questions = await _questionRepository.GetTaggedSortedQuestionsWithPaginationAsync(request.Tag, request.SortBy, request.SortDirection, request.Page, request.PageSize, QuestionState.Pending);
            var questionsPreview = questions.Select(question => Mapping.QuestionToQuestionPreviewDTO(question, authenticatedUserId)).ToList();

            return questionsPreview;
        }
    }
}
