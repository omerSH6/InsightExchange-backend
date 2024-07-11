using Application.Common.DTOs;
using Application.Common.Services.Mediator.Interfaces;
using Domain.Enums;
using Domain.Interfaces.Authentication;
using Domain.Interfaces.Repositories;

namespace Application.Questions.Queries
{
    public class GetPendingQuestionQuery : IRequest<QuestionDTO>
    {
        public int Id { get; set; }
    }

    public class GetPendingQuestionHandler : IRequestHandler<GetPendingQuestionQuery, QuestionDTO>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public GetPendingQuestionHandler(IQuestionRepository questionRepository, IUserRepository userRepository, IUserService userService)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<QuestionDTO> Handle(GetPendingQuestionQuery request)
        {
            var requestedQuestingId = request.Id;
            var authenticatedUserId = _userService.GetAuthenticatedUserId();
            var question = await _questionRepository.GetByIdAsync(requestedQuestingId, QuestionState.Pending);

            var user = await _userRepository.GetUserById(authenticatedUserId);
            if (user.Role != UserRole.Admin)
            {
                throw new Exception($"User NOT authorized");
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
                WasAskedByCurrentUser = question.UserId == authenticatedUserId,
                WasVotedByCurrentUser = question.Votes.Any(vote => vote.UserId == authenticatedUserId),
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
                    WasReipaiedByCurrentUser = answare.UserId == authenticatedUserId,
                    WasVotedByCurrentUser = answare.Votes.Any(vote =>vote.UserId == authenticatedUserId),
                    TotalVotes = answare.Votes.Where(vote => vote.isPositiveVote).ToList().Count() - answare.Votes.Where(vote => !vote.isPositiveVote).ToList().Count(),
                }).ToList(),
                Tags = question.Tags.Select(tag=> new TagDTO() { Name = tag.Name}).ToList(),
            };

            return questionDTO;
        }
    }
}
