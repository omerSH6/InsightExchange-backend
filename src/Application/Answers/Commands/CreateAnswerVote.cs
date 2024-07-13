using Application.Common.Interfaces;
using Application.Common.Services.Mediator.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Answers.Commands
{
    public class CreateAnswerVote : IRequest
    {
        public int AnswerId { get; set; }
        public bool IsPositiveVote { get; set; }
    }

    public class CreateAnswerVoteHandler : IRequestHandler<CreateAnswerVote>
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAnswerVoteRepository _AnswerVoteRepository;
        private readonly IUserService _userService;

        public CreateAnswerVoteHandler(IAnswerRepository answerRepository, IUserRepository userRepository, IAnswerVoteRepository answerVoteRepository, IUserService userService)
        {
            _answerRepository = answerRepository;
            _userRepository = userRepository;
            _AnswerVoteRepository = answerVoteRepository;
            _userService = userService;
        }

        public async Task Handle(CreateAnswerVote request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserId();
            var user = await _userRepository.GetUserByIdAsync(authenticatedUserId);
            var answer = await _answerRepository.GetAnswerByIdAsync(request.AnswerId);

            if (answer == null)
            {
                throw new Exception($"Answer with id: {request.AnswerId} does not exist");
            }

            if (answer.Votes.Any(v => v.UserId == user.Id))
            {
                throw new Exception($"User already voted for this question");
            }

            var answerVote = new AnswerVote()
            {
                CreatedAt = DateTime.UtcNow,
                isPositiveVote = request.IsPositiveVote,
                UserId = user.Id,
                User = user,
                AnswerId = answer.Id,
                Answer = answer
            };

            await _AnswerVoteRepository.CreateAnswerVoteAsync(answerVote);
        }
    }
}
