using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IQuestionVoteRepository
    {
        Task CreateQuestionVoteAsync(QuestionVote questionVote);
    }
}
