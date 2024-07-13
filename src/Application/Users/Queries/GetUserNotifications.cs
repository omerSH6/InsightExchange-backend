using Application.Common.DTOs;
using Application.Common.Services.Mediator.Interfaces;
using Domain.Interfaces.Authentication;
using Domain.Interfaces.Repositories;

namespace Application.Users.Queries
{
    public class GetUserNotificationsQuery : IRequest<List<UserNotificationDTO>>
    {
    }

    public class GetUserNotificationsHandler : IRequestHandler<GetUserNotificationsQuery, List<UserNotificationDTO>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public GetUserNotificationsHandler(IQuestionRepository questionRepository, IUserRepository userRepository, IUserService userService)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<List<UserNotificationDTO>> Handle(GetUserNotificationsQuery request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserId();
            var userNotifications = await _userRepository.GetUserNotifications(authenticatedUserId);

            return userNotifications.Select(n=> new UserNotificationDTO() { Content = n.Content, CreatedAt = n.CreatedAt}).ToList();
        }
    }
}
