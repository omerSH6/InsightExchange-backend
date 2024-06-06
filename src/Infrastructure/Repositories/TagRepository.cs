using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly InsightExchangeDbContext _context;

        public TagRepository(InsightExchangeDbContext context)
        {
            _context = context;
        }

        public async Task CreateTagAsync(string tagName)
        {
            var newTag = new Tag() { Name = tagName };
            _context.Tags.Add(newTag);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<List<Tag>> GetByNamesAsync(List<string> names)
        {
            return await _context.Tags.Where(tag=>names.Contains(tag.Name)).ToListAsync();
        }
    }
}
