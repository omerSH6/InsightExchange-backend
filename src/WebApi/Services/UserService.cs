using Domain.DTOs;
using System.Security.Claims;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserDTO> GetCurrentUserAsync()
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            if (userName == null)
            {
                return null;
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
            {
                return null;
            }

            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }
    }
}
