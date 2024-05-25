using Application.Services.Mediator.Interfaces;
using Domain.Entities;

namespace Application.Users.Queries
{
    public class GetUserQuery : IRequest
    {
    }

    public class GetUserHandler : IRequestHandler<GetUserQuery, User>
    {
        public Task<User> Handle(GetUserQuery request)
        {
            throw new NotImplementedException();
        }
    }
}
