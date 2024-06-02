using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly InsightExchangeDbContext _context;

        public AnswerRepository(InsightExchangeDbContext context)
        {
            _context = context;
        }

        public Task CreateAnswerAsync(Answer answer)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAnswerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Answer> GetAnswerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Answer>> GetAnswersByDiscussionIdAsync(int discussionId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAnswerAsync(Answer answer)
        {
            throw new NotImplementedException();
        }

        // Implement interface methods
    }
}
