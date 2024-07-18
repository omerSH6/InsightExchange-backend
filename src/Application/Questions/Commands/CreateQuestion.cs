using System.Diagnostics;
using Application.Answers.Commands;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using Application.Common.Services.Mediator.Interfaces;
using Application.Common.Utils;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;

namespace Application.Questions.Commands
{
    public class CreateQuestionCommand : IRequest<QuestionDTO>
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }

    public class CreateQuestionCommandValidator : IRequestValidator<CreateQuestionCommand>
    {
        public bool IsValid(CreateQuestionCommand request)
        {
            return Validators.IsShortStringValid(request.Title) && 
                    Validators.IsLongStringValid(request.Content) &&
                    Validators.IsTagsListToCreateValid(request.Tags);
        }
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

            return Mapping.QuestionToQuestionDTO(question, authenticatedUserId);
        }
    }
}
