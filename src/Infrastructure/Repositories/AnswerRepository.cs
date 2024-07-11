using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly InsightExchangeDbContext _context;

        public AnswerRepository(InsightExchangeDbContext context)
        {
            _context = context;
        }

        public async Task CreateAnswerAsync(Answer answer)
        {
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
        }

        public async Task<Answer> GetAnswerByIdAsync(int id)
        {
            return await _context.Answers.Include(a => a.Votes).Include(a => a.User).FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
