using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByUserNameAsync(string userName);
        Task<User> GetUserById(int id);
        Task AddUserAsync(User user);
        Task<List<Notification>> GetUserNotifications(int userId);
        Task AddUserNotification(int userId, Notification notification);
    }
}
