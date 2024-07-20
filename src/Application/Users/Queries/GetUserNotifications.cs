using Application.Common.DTOs;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Services.Mediator.Interfaces;
using Domain.Interfaces.Repositories;

namespace Application.Users.Queries
{
    public class GetUserNotificationsQuery : IRequest<List<UserNotificationDTO>>
    {
        public required int UserId { get; set; }
    }

    public class GetUserNotificationsQueryHandler : IRequestHandler<GetUserNotificationsQuery, List<UserNotificationDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public GetUserNotificationsQueryHandler(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<List<UserNotificationDTO>> Handle(GetUserNotificationsQuery request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserId();
            if (request.UserId != authenticatedUserId)
            {
                throw new UnauthorizedException();
            }
            var userNotifications = await _userRepository.GetUserNotifications(authenticatedUserId);

            return userNotifications.Select(n=> new UserNotificationDTO() { Content = n.Content, CreatedAt = n.CreatedAt}).ToList();
        }
    }
}
