using Application.Services.Mediator.Interfaces;
using Domain.Interfaces;
using Domain.Entities;
using Application.Services.Mediator;

namespace Application.Answers.Commands
{
    public class CreateAnswerCommand : IRequest
    {        
        public string Content { get; set; }
        public int QuestionId { get; set; }
    }

    public class CreateAnswerHandler : IRequestHandler<CreateAnswerCommand>
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IQuestionRepository _questionRepository;


        public CreateAnswerHandler(IAnswerRepository answerRepository, IUserRepository userRepository, IQuestionRepository questionRepository)
        {
            _answerRepository = answerRepository;
            _userRepository = userRepository;
            _questionRepository = questionRepository;
        }

        async Task<ResultDto<bool>> IRequestHandler<CreateAnswerCommand>.Handle(CreateAnswerCommand request)
        {
            var authenticatedUserId = 1123;
            var user = await _userRepository.GetUserByIdAsync(authenticatedUserId);
            var question = await _questionRepository.GetByIdAsync(request.QuestionId);

            if (question == null)
            {
                return ResultDto<bool>.Fail($"Question with id: {request.QuestionId} does not exist");
            }

            var answer = new Answer() 
            {
                UserId = user.Id,
                User = user,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow,
                QuestionId = question.Id,
                Question = question,
            };

            await _answerRepository.CreateAnswerAsync(answer);
            return ResultDto<bool>.Success(true);
        }
    }
}
