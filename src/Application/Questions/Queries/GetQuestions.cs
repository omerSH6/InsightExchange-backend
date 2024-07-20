using Application.Common.DTOs;
using Application.Common.Interfaces;
using Application.Common.Services.Mediator.Interfaces;
using Application.Common.Utils;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;

namespace Application.Questions.Queries
{
    public class GetQuestionsQuery : IRequest<List<QuestionPreviewDTO>>
    {
        public string? Tag { get; set; }
        public required SortBy SortBy { get; set; }
        public required SortDirection SortDirection { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class GetQuestionsQueryValidator : IRequestValidator<GetQuestionsQuery>
    {
        public bool IsValid(GetQuestionsQuery request)
        {
            return Validators.IsIdValid(request.Page) && Validators.IsIdValid(request.PageSize);
        }
    }

    public class GetQuestionsQueryHandler : IRequestHandler<GetQuestionsQuery, List<QuestionPreviewDTO>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public GetQuestionsQueryHandler(IQuestionRepository questionRepository, IUserRepository userRepository, IUserService userService)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<List<QuestionPreviewDTO>> Handle(GetQuestionsQuery request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserIfExist();
            var questions = await _questionRepository.GetTaggedSortedQuestionsWithPaginationAsync(request.Tag, request.SortBy, request.SortDirection, request.Page, request.PageSize, QuestionState.Approved);
            var questionsPreview = questions.Select(question => Mapping.QuestionToQuestionPreviewDTO(question, authenticatedUserId)).ToList();

            return questionsPreview;
        }
    }
}
