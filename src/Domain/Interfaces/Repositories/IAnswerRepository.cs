using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IAnswerRepository
    {
        Task<Answer> GetAnswerByIdAsync(int id);
        Task CreateAnswerAsync(Answer answer);
        Task DeleteAnswer(int answerId);
    }
}
