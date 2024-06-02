using Application.Services.Mediator;
using Application.Services.Mediator.Interfaces;
using Domain.DTOs;
using Domain.Interfaces;

namespace Application.Questions.Queries
{
    public class GetQuestionQuery : IRequest<QuestionDTO>
    {
        public int Id { get; set; }
    }

    public class GetDiscussionHandler : IRequestHandler<GetQuestionQuery, QuestionDTO>
    {
        private readonly IQuestionRepository _questionRepository;

        public GetDiscussionHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<ResultDto<QuestionDTO>> Handle(GetQuestionQuery request)
        {
            var requestedQuestingId = request.Id;
            var authenticatedUserId = 1123;
            var question = await _questionRepository.GetByIdAsync(requestedQuestingId);
           

            var questionDTO = new QuestionDTO()
            {
                Id = question.Id,
                Content = question.Content,
                CreatedAt = question.CreatedAt,
                User = new UserDTO()
                {
                    Id = question.User.Id,
                    UserName = question.User.UserName
                },
                WasAskedByCurrentUser = question.User.Id == authenticatedUserId,
                WasVotedByCurrentUser = question.Votes.Any(vote => vote.User.Id == authenticatedUserId),
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
                    WasReipaiedByCurrentUser = answare.Id == authenticatedUserId,
                    WasVotedByCurrentUser = answare.Votes.Any(vote => vote.User.Id == authenticatedUserId),
                    TotalVotes = answare.Votes.Where(vote => vote.isPositiveVote).ToList().Count() - answare.Votes.Where(vote => !vote.isPositiveVote).ToList().Count(),
                }).ToList(),
            };

            return ResultDto<QuestionDTO>.Success(questionDTO);
        }
    }
}
