using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class QuestionVoteRepository : IQuestionVoteRepository
    {
        public Task CreateQuestionVoteAsync(QuestionVote questionVote)
        {
            throw new NotImplementedException();
        }
    }
}
