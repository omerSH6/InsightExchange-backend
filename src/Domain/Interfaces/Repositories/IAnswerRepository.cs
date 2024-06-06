using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IAnswerRepository
    {
        Task<Answer> GetAnswerByIdAsync(int id);
        Task<IEnumerable<Answer>> GetAnswersByDiscussionIdAsync(int discussionId);
        Task CreateAnswerAsync(Answer answer);
        Task UpdateAnswerAsync(Answer answer);
        Task DeleteAnswerAsync(int id);
    }
}
