using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class QuestionRepository : IDiscussionRepository
    {
        private readonly InsightExchangeDbContext _context;

        public QuestionRepository(InsightExchangeDbContext context)
        {
            _context = context;
        }

        public async Task<Question> GetDiscussionByIdAsync(int id)
        {
            return await _context.Discussions.Include(d => d.Tags).Include(d => d.Answers).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Question>> GetDiscussionsAsync()
        {
            return await _context.Discussions.Include(d => d.Tags).ToListAsync();
        }

        public async Task AddDiscussionAsync(Question discussion)
        {
            _context.Discussions.Add(discussion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDiscussionAsync(Question discussion)
        {
            _context.Entry(discussion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDiscussionAsync(int id)
        {
            var discussion = await _context.Discussions.FindAsync(id);
            _context.Discussions.Remove(discussion);
            await _context.SaveChangesAsync();
        }
    }
}
