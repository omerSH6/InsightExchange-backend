using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces.Repositories
{
    public interface IQuestionRepository
    {
        public Task<Question> GetByIdAsync(int id);
        public Task<List<Question>> GetTaggedSortedQuestionsWithPaginationAsync(string? tagName, SortBy sortBy, SortDirection sortDirection, int page, int pageSize);
        Task CreateQuestionAsync(Question answer);
    }
}
