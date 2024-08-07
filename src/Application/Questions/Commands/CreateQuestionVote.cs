﻿using Domain.Entities;
using Domain.Interfaces.Repositories;
using Application.Common.Services.Mediator.Interfaces;
using Domain.Enums;
using Application.Common.Interfaces;
using Application.Common.Utils;
using Application.Common.Exceptions;

namespace Application.Questions.Commands
{
    public class CreateQuestionVoteCommand : IRequest
    {
        public int QuestionId { get; set; }
        public bool IsPositiveVote { get; set; }
    }


    public class CreateQuestionVoteCommandValidator : IRequestValidator<CreateQuestionVoteCommand>
    {
        public bool IsValid(CreateQuestionVoteCommand request)
        {
            return Validators.IsIdValid(request.QuestionId);
        }
    }

    public class CreateQuestionVoteCommandHandler : IRequestHandler<CreateQuestionVoteCommand>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IQuestionVoteRepository _questionVoteRepository;
        private readonly IUserService _userService;

        public CreateQuestionVoteCommandHandler(IQuestionRepository questionRepository, IUserRepository userRepository, IQuestionVoteRepository questionVoteRepository, IUserService userService)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _questionVoteRepository = questionVoteRepository;
            _userService = userService;
        }

        public async Task Handle(CreateQuestionVoteCommand request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserId();
            var user = await _userRepository.GetUserByIdAsync(authenticatedUserId);
            var question = await _questionRepository.GetByIdAsync(request.QuestionId, QuestionState.Approved);

            if (question == null)
            {
                throw new OperationFailedException();
            }

            if (question.Votes.Any(v=>v.UserId == user.Id))
            {
                throw new Exception($"User already voted for this question");
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
        }
    }
}
