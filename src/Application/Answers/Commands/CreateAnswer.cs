using Application.Common.DTOs;
using Application.Common.Services.Mediator.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Authentication;
using Domain.Interfaces.Repositories;
using Domain.Shared;

namespace Application.Answers.Commands
{
    public class CreateAnswerCommand : IRequest<AnswerDTO>
    {        
        public required string Content { get; set; }
        public int QuestionId { get; set; }
    }

    public class CreateAnswerHandler : IRequestHandler<CreateAnswerCommand, AnswerDTO>
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserService _userService;

        public CreateAnswerHandler(IAnswerRepository answerRepository, IUserRepository userRepository, IQuestionRepository questionRepository, IUserService userService)
        {
            _answerRepository = answerRepository;
            _userRepository = userRepository;
            _questionRepository = questionRepository;
            _userService = userService;
        }

        async Task<AnswerDTO> IRequestHandler<CreateAnswerCommand, AnswerDTO>.Handle(CreateAnswerCommand request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserId();
            var user = await _userRepository.GetUserByIdAsync(authenticatedUserId);
            var question = await _questionRepository.GetByIdAsync(request.QuestionId);

            if (question == null)
            {
                throw new Exception($"Question with id: {request.QuestionId} does not exist");
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

            var answerDto = new AnswerDTO() 
            {
                Id = answer.Id,
                Content = answer.Content,
                CreatedAt = answer.CreatedAt,
                User = new UserDTO()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                },
                WasReipaiedByCurrentUser = true,
                WasVotedByCurrentUser = false,
                TotalVotes = 0

            };

            return answerDto;
        }
    }
}
