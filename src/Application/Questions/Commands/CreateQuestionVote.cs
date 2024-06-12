using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Shared;
using Domain.Interfaces.Authentication;
using Application.Common.Services.Mediator.Interfaces;

namespace Application.Questions.Commands
{
    public class CreateQuestionVote : IRequest
    {
        public int QuestionId { get; set; }
        public bool IsPositiveVote { get; set; }
    }

    public class CreateQuestionVoteHandler : IRequestHandler<CreateQuestionVote>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IQuestionVoteRepository _questionVoteRepository;
        private readonly IUserService _userService;

        public CreateQuestionVoteHandler(IQuestionRepository questionRepository, IUserRepository userRepository, IQuestionVoteRepository questionVoteRepository, IUserService userService)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _questionVoteRepository = questionVoteRepository;
            _userService = userService;
        }

        public async Task<ResultDto<bool>> Handle(CreateQuestionVote request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserId();
            var user = await _userRepository.GetUserByIdAsync(authenticatedUserId);
            var question = await _questionRepository.GetByIdAsync(request.QuestionId);

            if (question == null)
            {
                return ResultDto<bool>.Fail($"Question with id: {request.QuestionId} does not exist");
            }

            var questionVote = new QuestionVote() 
            {
                CreatedAt = DateTime.UtcNow,
                isPositiveVote = request.IsPositiveVote,
                UserId = user.Id,
                User = user,
                QuestionId = question.Id,
                Question = question
            };

            await _questionVoteRepository.CreateQuestionVoteAsync(questionVote);
            return ResultDto<bool>.Success(true);
        }
    }
}
