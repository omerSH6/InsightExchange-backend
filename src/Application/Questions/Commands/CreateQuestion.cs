using System;
using Application.Common.DTOs;
using Application.Common.Services.Mediator.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Authentication;
using Domain.Interfaces.Repositories;
using Domain.Shared;

namespace Application.Questions.Commands
{
    public class CreateQuestionCommand : IRequest<QuestionDTO>
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }

    public class CreateQuestionHandler : IRequestHandler<CreateQuestionCommand, QuestionDTO>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IUserService _userService;

        public CreateQuestionHandler(IQuestionRepository questionRepository, IUserRepository userRepository, ITagRepository tagRepository, IUserService userService)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _tagRepository = tagRepository;
            _userService = userService;
        }

        public async Task<QuestionDTO> Handle(CreateQuestionCommand request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserId();
            var user = await _userRepository.GetUserByIdAsync(authenticatedUserId);
            var existingTags = await _tagRepository.GetAllTagsAsync();

            foreach (var tag in request.Tags)
            {
                if (!existingTags.Select(tag=>tag.Name).Contains(tag))
                {
                    await _tagRepository.CreateTagAsync(tag);
                }
            }
            var questionTags = await _tagRepository.GetByNamesAsync(request.Tags);

            var question = new Question()
            {
                Title = request.Title,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id,
                User = user,
                Tags = questionTags,
                State = QuestionState.Pending,
            };

            await _questionRepository.CreateQuestionAsync(question);

            var questionDto = new QuestionDTO() 
            {
                Id = question.Id,
                Title = question.Title,
                Content = question.Content,
                CreatedAt = question.CreatedAt,
                User = new UserDTO()
                {
                    Id=user.Id,
                    UserName = user.UserName,

                },
                WasAskedByCurrentUser = true,
                WasVotedByCurrentUser = false,
                TotalVotes = 0,
                Answers = new List<AnswerDTO>(),
                Tags = question.Tags.Select(tag => new TagDTO() 
                {
                    Name = tag.Name,
                }).ToList(),
            };
            return questionDto;
        }
    }
}
