﻿using Application.Common.Services.Mediator.Interfaces;
using Domain.Enums;
using Domain.Interfaces.Authentication;
using Domain.Interfaces.Repositories;

namespace Application.Questions.Commands
{
    public class DeleteQuestionCommand : IRequest
    {
        public int QuestionId { get; set; }
    }

    public class DeleteQuestionHandler : IRequestHandler<DeleteQuestionCommand>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public DeleteQuestionHandler(IQuestionRepository questionRepository, IUserRepository userRepository, IUserService userService)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task Handle(DeleteQuestionCommand request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserId();

            var user = await _userRepository.GetUserById(authenticatedUserId);
            if (user.Role != UserRole.Admin)
            {
                throw new Exception($"User NOT authorized");
            }

            await _questionRepository.DeleteQuestion(request.QuestionId);
        }
    }
}
