﻿using Application.Common.Services.Mediator.Interfaces;
using Domain.Enums;
using Domain.Interfaces.Authentication;
using Domain.Interfaces.Repositories;

namespace Application.Answers.Commands
{
    public class DeleteAnswerCommand : IRequest
    {
        public int AnswerId { get; set; }
    }

    public class DeleteAnswerHandler : IRequestHandler<DeleteAnswerCommand>
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public DeleteAnswerHandler(IAnswerRepository answerRepository, IUserRepository userRepository, IUserService userService)
        {
            _answerRepository = answerRepository;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task Handle(DeleteAnswerCommand request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserId();

            var user = await _userRepository.GetUserById(authenticatedUserId);
            if (user.Role != UserRole.Admin)
            {
                throw new Exception($"User NOT authorized");
            }

            await _answerRepository.DeleteAnswer(request.AnswerId);
        }
    }
}