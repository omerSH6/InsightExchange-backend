using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDiscussionRepository
    {
        Task<Question> GetDiscussionByIdAsync(int id);
        Task<IEnumerable<Question>> GetDiscussionsAsync();
        Task AddDiscussionAsync(Question discussion);
        Task UpdateDiscussionAsync(Question discussion);
        Task DeleteDiscussionAsync(int id);
    }
}
