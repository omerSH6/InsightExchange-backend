using Application.Services.Mediator.Interfaces;
using Domain.Entities;

namespace Application.Questions.Queries
{
    public class GetQuestionsWithPaginationQuery : IRequest<Question>
    {
    }

    public class GetQuestionsWithPaginationHandler : IRequestHandler<GetQuestionsWithPaginationQuery, Question>
    {
        public Task<Question> Handle(GetQuestionsWithPaginationQuery request)
        {
            throw new NotImplementedException();
        }
    }
}
