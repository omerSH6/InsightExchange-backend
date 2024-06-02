using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAnswerVoteRepository
    {
        Task CreateAnswerVoteAsync(AnswerVote answerVote);
    }
}
