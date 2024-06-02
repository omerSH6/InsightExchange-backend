using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IQuestionVoteRepository
    {
        Task CreateQuestionVoteAsync(QuestionVote questionVote);
    }
}
