using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        public Task CreateTagAsync(string tagName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Tag>> GetAllTagsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tag> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<Tag>> GetByNamesAsync(List<string> names)
        {
            throw new NotImplementedException();
        }
    }
}
