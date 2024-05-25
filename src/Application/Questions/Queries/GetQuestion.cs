using Application.Services.Mediator.Interfaces;
using Domain.Entities;

namespace Application.Questions.Queries
{
    public class GetDiscussionQuery : IRequest<Question>
    {
       
    }

    public class GetDiscussionHandler : IRequestHandler<GetDiscussionQuery, Question>
    {
        public Task<Question> Handle(GetDiscussionQuery request)
        {
            throw new NotImplementedException();
        }
    }
}
