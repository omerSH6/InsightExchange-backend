using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly InsightExchangeDbContext _context;

        public QuestionRepository(InsightExchangeDbContext context)
        {
            _context = context;
        }

        public async Task<Question> GetByIdAsync(int id)
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

        public Task<List<Question>> GetTaggedSortedQuestionsWithPaginationAsync(string? tagName, SortBy sortBy, SortDirection sortDirection, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task CreateQuestionAsync(Question answer)
        {
            throw new NotImplementedException();
        }
    }
}
