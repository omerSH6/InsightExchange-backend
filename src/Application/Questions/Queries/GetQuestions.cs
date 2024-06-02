using Application.Services.Mediator;
using Application.Services.Mediator.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Questions.Queries
{
    public class GetQuestionsQuery : IRequest<List<QuestionPreviewDTO>>
    {
        public required string Tag { get; set; }
        public required SortBy SortBy { get; set; }
        public required SortDirection SortDirection { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class GetQuestionsHandler : IRequestHandler<GetQuestionsQuery, List<QuestionPreviewDTO>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITagRepository _tagRepository;

        public GetQuestionsHandler(IQuestionRepository questionRepository, IUserRepository userRepository, ITagRepository tagRepository)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _tagRepository = tagRepository;
        }

        public async Task<ResultDto<List<QuestionPreviewDTO>>> Handle(GetQuestionsQuery request)
        {
            var authenticatedUserId = 1123;

            var user = await _userRepository.GetUserByIdAsync(authenticatedUserId);
            var tag = await _tagRepository.GetByNameAsync(request.Tag);

            var questions = await _questionRepository.GetTaggedSortedQuestionsWithPaginationAsync(tag, request.SortBy, request.SortDirection, request.Page, request.PageSize);
            var questionsPreview = questions.Select(question => QuestionToQuestionPreviewDTO(question, authenticatedUserId)).ToList();

            return ResultDto<List<QuestionPreviewDTO>>.Success(questionsPreview);
        }

        private static QuestionPreviewDTO QuestionToQuestionPreviewDTO(Question question, int AuthenticatedUserId)
        {
            var userDto = new UserDTO() 
            {
                Id = question.User.Id,
                UserName = question.User.UserName,
            };

            var questionPreviewDTO = new QuestionPreviewDTO()
            {
                Id = question.Id,
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
