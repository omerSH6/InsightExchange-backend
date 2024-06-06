using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class QuestionVoteRepository : IQuestionVoteRepository
    {
        private readonly InsightExchangeDbContext _context;

        public QuestionVoteRepository(InsightExchangeDbContext context)
        {
            _context = context;
        }

        public async Task CreateQuestionVoteAsync(QuestionVote questionVote)
        {
            _context.QuestionVote.Add(questionVote);
            await _context.SaveChangesAsync();
        }
    }
}
