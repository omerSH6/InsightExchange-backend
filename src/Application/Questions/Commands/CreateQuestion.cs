using Application.Services.Mediator;
using Application.Services.Mediator.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Questions.Commands
{
    public class CreateQuestionCommand : IRequest
    {
        public required string Content { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }

    public class CreateQuestionHandler : IRequestHandler<CreateQuestionCommand>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITagRepository _tagRepository;

        public CreateQuestionHandler(IQuestionRepository questionRepository, IUserRepository userRepository, ITagRepository tagRepository)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _tagRepository = tagRepository;
        }

        public async Task<ResultDto<bool>> Handle(CreateQuestionCommand request)
        {
            var authenticatedUserId = 1123;

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
                Content = request.Content,
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id,
                User = user,
                Tags = questionTags
            };

            await _questionRepository.CreateQuestionAsync(question);

            return ResultDto<bool>.Success(true);
        }
    }
}
