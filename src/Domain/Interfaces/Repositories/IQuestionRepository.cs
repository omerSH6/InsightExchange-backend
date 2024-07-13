using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces.Repositories
{
    public interface IQuestionRepository
    {
        Task<Question> GetByIdAsync(int id, QuestionState questionState);
        Task<Question> GetByIdAsync(int id);
        Task<List<Question>> GetTaggedSortedQuestionsWithPaginationAsync(string? tagName, SortBy sortBy, SortDirection sortDirection, int page, int pageSize, QuestionState questionState);
        Task CreateQuestionAsync(Question answer);
        Task EditQuestionState(int questionId, QuestionState questionState);
        Task DeleteQuestion(int questionId);
    }
}
