using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class AnswerVoteRepository : IAnswerVoteRepository
    {
        private readonly InsightExchangeDbContext _context;

        public AnswerVoteRepository(InsightExchangeDbContext context)
        {
            _context = context;
        }

        public async Task CreateAnswerVoteAsync(AnswerVote answerVote)
        {
            _context.AnswerVote.Add(answerVote);
            await _context.SaveChangesAsync();
        }
    }
}
