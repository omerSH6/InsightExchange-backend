using Domain.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class DiscussionService
    {
        private readonly IDiscussionRepository _discussionRepository;

        public DiscussionService(IDiscussionRepository discussionRepository)
        {
            _discussionRepository = discussionRepository;
        }

        public async Task<Question> GetDiscussionByIdAsync(int id)
        {
            return await _discussionRepository.GetDiscussionByIdAsync(id);
        }

        public async Task<IEnumerable<Question>> GetDiscussionsAsync()
        {
            return await _discussionRepository.GetDiscussionsAsync();
        }

        public async Task AddDiscussionAsync(Question discussion)
        {
            await _discussionRepository.AddDiscussionAsync(discussion);
        }

        public async Task UpdateDiscussionAsync(Question discussion)
        {
            await _discussionRepository.UpdateDiscussionAsync(discussion);
        }

        public async Task DeleteDiscussionAsync(int id)
        {
            await _discussionRepository.DeleteDiscussionAsync(id);
        }
    }

}
