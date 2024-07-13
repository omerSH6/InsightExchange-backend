using Application.Common.DTOs;
using Application.Common.Interfaces;
using Application.Common.Services.Mediator.Interfaces;
using Domain.Entities;
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

    public class GetPendingQuestionsHandler : IRequestHandler<GetPendingQuestionsQuery, List<QuestionPreviewDTO>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public GetPendingQuestionsHandler(IQuestionRepository questionRepository, IUserRepository userRepository, IUserService userService)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<List<QuestionPreviewDTO>> Handle(GetPendingQuestionsQuery request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserId();

            var user = await _userRepository.GetUserById(authenticatedUserId);
            if (user.Role != UserRole.Admin)
            {
                throw new Exception($"User NOT authorized");
            }

            var questions = await _questionRepository.GetTaggedSortedQuestionsWithPaginationAsync(request.Tag, request.SortBy, request.SortDirection, request.Page, request.PageSize, QuestionState.Pending);
            var questionsPreview = questions.Select(question => QuestionToQuestionPreviewDTO(question, authenticatedUserId)).ToList();

            return questionsPreview;
        }

        private static QuestionPreviewDTO QuestionToQuestionPreviewDTO(Question question, int? AuthenticatedUserId)
        {
            var userDto = new UserDTO() 
            {
                Id = question.User.Id,
                UserName = question.User.UserName,
            };

            var questionPreviewDTO = new QuestionPreviewDTO()
            {
                Id = question.Id,
                Title = question.Title,
                PreviewContent = question.Content.Length > 20 ? question.Content.Take(20).ToString() : question.Content,
                CreatedAt = question.CreatedAt,
                User = userDto,
                WasAskedByCurrentUser = question.UserId == AuthenticatedUserId,
                WasVotedByCurrentUser = question.Votes.Any(vote => vote.User.Id == AuthenticatedUserId),
                TotalVotes = question.Votes.Where(vote => vote.isPositiveVote).ToList().Count() - question.Votes.Where(vote => !vote.isPositiveVote).ToList().Count(),
                TotalAnswers = question.Answers.Count(),
                Tags = question.Tags.Select(tag => new TagDTO()
                {
                    Name = tag.Name
                }).ToList()
            };

            return questionPreviewDTO;
        }
    }
}
