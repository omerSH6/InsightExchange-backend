using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAnswerRepository
    {
        Task<Answer> GetAnswerByIdAsync(int id);
        Task<IEnumerable<Answer>> GetAnswersByDiscussionIdAsync(int discussionId);
        Task AddAnswerAsync(Answer answer);
        Task UpdateAnswerAsync(Answer answer);
        Task DeleteAnswerAsync(int id);
    }
}
