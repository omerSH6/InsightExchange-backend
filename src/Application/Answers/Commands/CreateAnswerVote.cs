using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Services.Mediator.Interfaces;
using Application.Common.Utils;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Answers.Commands
{
    public class CreateAnswerVoteCommand : IRequest
    {
        public int AnswerId { get; set; }
        public bool IsPositiveVote { get; set; }
    }

    public class CreateAnswerVoteCommandValidator : IRequestValidator<CreateAnswerVoteCommand>
    {
        public bool IsValid(CreateAnswerVoteCommand request)
        {
            return Validators.IsIdValid(request.AnswerId);
        }
    }

    public class CreateAnswerVoteCommandHandler : IRequestHandler<CreateAnswerVoteCommand>
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAnswerVoteRepository _AnswerVoteRepository;
        private readonly IUserService _userService;

        public CreateAnswerVoteCommandHandler(IAnswerRepository answerRepository, IUserRepository userRepository, IAnswerVoteRepository answerVoteRepository, IUserService userService)
        {
            _answerRepository = answerRepository;
            _userRepository = userRepository;
            _AnswerVoteRepository = answerVoteRepository;
            _userService = userService;
        }

        public async Task Handle(CreateAnswerVoteCommand request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserId();
            var user = await _userRepository.GetUserByIdAsync(authenticatedUserId);
            var answer = await _answerRepository.GetAnswerByIdAsync(request.AnswerId);

            if (answer == null)
            {
                throw new OperationFailedException();
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
