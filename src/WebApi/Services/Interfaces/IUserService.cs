using Domain.DTOs;

namespace WebApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetCurrentUserAsync();
    }
}
