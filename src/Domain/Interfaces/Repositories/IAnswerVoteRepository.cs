using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IAnswerVoteRepository
    {
        Task CreateAnswerVoteAsync(AnswerVote answerVote);
    }
}
