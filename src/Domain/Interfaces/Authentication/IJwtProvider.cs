using Domain.Entities;

namespace Domain.Interfaces.Authentication
{
    public interface IJwtProvider
    {
        string Generate(User member);
    }
}
