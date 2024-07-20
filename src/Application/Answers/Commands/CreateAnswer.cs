using Application.Common.DTOs;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Services.Mediator.Interfaces;
using Application.Common.Utils;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;

namespace Application.Answers.Commands
{
    public class CreateAnswerCommand : IRequest<AnswerDTO>
    {        
        public required string Content { get; set; }
        public int QuestionId { get; set; }
    }

    public class CreateAnswerCommandValidator : IRequestValidator<CreateAnswerCommand>
    {
        public bool IsValid(CreateAnswerCommand request)
        {
            return Validators.IsLongStringValid(request.Content) && Validators.IsIdValid(request.QuestionId);
        }
    }

    public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, AnswerDTO>
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserService _userService;

        public CreateAnswerCommandHandler(IAnswerRepository answerRepository, IUserRepository userRepository, IQuestionRepository questionRepository, IUserService userService)
        {
            _answerRepository = answerRepository;
            _userRepository = userRepository;
            _questionRepository = questionRepository;
            _userService = userService;
        }

        public async Task<AnswerDTO> Handle(CreateAnswerCommand request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserId();
            var user = await _userRepository.GetUserByIdAsync(authenticatedUserId);
            var question = await _questionRepository.GetByIdAsync(request.QuestionId, QuestionState.Approved);

            if (question == null)
            {
                throw new OperationFailedException();
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

          
            return Mapping.AnswerToAnswerDTO(answer, authenticatedUserId);
        }
    }
}
