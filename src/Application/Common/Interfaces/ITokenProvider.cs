using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface ITokenProvider
    {
        string Generate(User member);
    }
}
