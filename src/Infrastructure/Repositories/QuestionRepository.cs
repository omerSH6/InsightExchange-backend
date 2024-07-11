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
            return await _context.Questions.Include(d => d.Tags).Include(d => d.Answers).ThenInclude(a=>a.User).Include(q=>q.Answers).ThenInclude(a=>a.Votes).Include(q=>q.Votes).Include(d => d.User).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<Question>> GetTaggedSortedQuestionsWithPaginationAsync(string? tagName, SortBy sortBy, SortDirection sortDirection, int page, int pageSize)
        {
            // Start building the query
            var query = _context.Questions.AsQueryable();

            // Apply filtering by tag name if provided
            if (!string.IsNullOrWhiteSpace(tagName))
            {
                query = query.Where(q => q.Tags.Select(tag=>tag.Name).Contains(tagName));
            }

            // Apply sorting
            switch (sortBy)
            {
                case SortBy.CreationDate:
                    query = sortDirection == SortDirection.Ascending ? query.OrderBy(q => q.CreatedAt) : query.OrderByDescending(q => q.CreatedAt);
                    break;
                default:
                    throw new ArgumentException("Invalid sort by option.", nameof(sortBy));
            }

            // Apply pagination
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            // Execute the query and return the result
            query = query.Include(q => q.User).Include(q => q.Tags).Include(q => q.Answers);
            return await query.ToListAsync();
        }

        public async Task CreateQuestionAsync(Question answer)
        {
            _context.Questions.Add(answer);
            await _context.SaveChangesAsync();
        }
    }
}
