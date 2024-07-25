using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface ITagRepository
    {
       Task CreateTagAsync(string tagName);
       Task<List<Tag>> GetAllTagsAsync();
       Task<List<Tag>> GetByNamesAsync(List<string> names);
    }
}
