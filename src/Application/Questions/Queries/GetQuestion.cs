using Application.Common.DTOs;
using Application.Common.Services.Mediator.Interfaces;
using Domain.Interfaces.Authentication;
using Domain.Interfaces.Repositories;
using Domain.Shared;

namespace Application.Questions.Queries
{
    public class GetQuestionQuery : IRequest<QuestionDTO>
    {
        public int Id { get; set; }
    }

    public class GetDiscussionHandler : IRequestHandler<GetQuestionQuery, QuestionDTO>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserService _userService;

        public GetDiscussionHandler(IQuestionRepository questionRepository, IUserService userService)
        {
            _questionRepository = questionRepository;
            _userService = userService;
        }

        public async Task<ResultDto<QuestionDTO>> Handle(GetQuestionQuery request)
        {
            var requestedQuestingId = request.Id;
            var authenticatedUserId = _userService.GetAuthenticatedUserIfExist();
            var question = await _questionRepository.GetByIdAsync(requestedQuestingId);

            if (question == null)
            {
                return ResultDto<QuestionDTO>.Fail($"The question with the id of {requestedQuestingId} not exist");
            }

            var questionDTO = new QuestionDTO()
            {
                Title = question.Title,
                Id = question.Id,
                Content = question.Content,
                CreatedAt = question.CreatedAt,
                User = new UserDTO()
                {
                    Id = question.User.Id,
                    UserName = question.User.UserName
                },
                WasAskedByCurrentUser = authenticatedUserId.HasValue && question.User.Id == authenticatedUserId,
                WasVotedByCurrentUser = question.Votes.Any(vote => authenticatedUserId.HasValue && vote.User.Id == authenticatedUserId),
                TotalVotes = question.Votes.Where(vote => vote.isPositiveVote).ToList().Count() - question.Votes.Where(vote => !vote.isPositiveVote).ToList().Count(),
                Answers = question.Answers.Select(answare => new AnswerDTO()
                {
                    Id = answare.Id,
                    Content = answare.Content,
                    CreatedAt = answare.CreatedAt,
                    User = new UserDTO()
                    {
                        Id = answare.User.Id,
                        UserName = answare.User.UserName
                    },
                    WasReipaiedByCurrentUser = authenticatedUserId.HasValue && answare.Id == authenticatedUserId,
                    WasVotedByCurrentUser = answare.Votes.Any(vote =>authenticatedUserId.HasValue &&  vote.User.Id == authenticatedUserId),
                    TotalVotes = answare.Votes.Where(vote => vote.isPositiveVote).ToList().Count() - answare.Votes.Where(vote => !vote.isPositiveVote).ToList().Count(),
                }).ToList(),
                Tags = question.Tags.Select(tag=> new TagDTO() { Name = tag.Name, Id = tag.Id}).ToList(),
            };

            return ResultDto<QuestionDTO>.Success(questionDTO);
        }
    }
}
