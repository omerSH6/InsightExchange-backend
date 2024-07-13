using System.Security.Claims;
using Application.Common.Interfaces;

namespace WebApi.Authentication
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? GetAuthenticatedUserIfExist()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var parseResult = int.TryParse(userId, out var result);
            return parseResult ? result : null;
        }

        int IUserService.GetAuthenticatedUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return int.Parse(userId);
        }
    }
}
