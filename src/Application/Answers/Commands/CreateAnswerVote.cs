using Application.Services.Mediator.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Authentication;
using Domain.Interfaces.Repositories;
using Domain.Shared;

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

        public async Task<ResultDto<bool>> Handle(CreateAnswerVote request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserId();
            var user = await _userRepository.GetUserByIdAsync(authenticatedUserId);
            var answer = await _answerRepository.GetAnswerByIdAsync(request.AnswerId);

            if (answer == null)
            {
                return ResultDto<bool>.Fail($"Answer with id: {request.AnswerId} does not exist");
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
            return ResultDto<bool>.Success(true);
        }
    }
}
