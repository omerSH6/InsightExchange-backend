using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IQuestionRepository
    {
        public Task<Question> GetByIdAsync(int id);
        public Task<List<Question>> GetTaggedSortedQuestionsWithPaginationAsync(Tag tag, SortBy sortBy, SortDirection sortDirection, int page, int pageSize);
        Task CreateQuestionAsync(Question answer);
    }
}
