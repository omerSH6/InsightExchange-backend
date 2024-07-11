using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces.Repositories
{
    public interface IQuestionRepository
    {
        public Task<Question> GetByIdAsync(int id, QuestionState questionState);
        public Task<List<Question>> GetTaggedSortedQuestionsWithPaginationAsync(string? tagName, SortBy sortBy, SortDirection sortDirection, int page, int pageSize, QuestionState questionState);
        Task CreateQuestionAsync(Question answer);
    }
}
