using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly InsightExchangeDbContext _context;

        public UserRepository(InsightExchangeDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.Where(user => user.Email.Equals(email)).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.Where(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.Where(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.Where(user => user.UserName.Equals(userName)).FirstOrDefaultAsync();
        }
    }
}
