using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUserNameAsync(string userName);
        Task AddUserAsync(User user);
        Task<List<Notification>> GetUserNotifications(int userId);
        Task AddUserNotification(int userId, Notification notification);
    }
}
