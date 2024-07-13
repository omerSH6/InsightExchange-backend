namespace Application.Common.Interfaces
{
    public interface IUserService
    {
        int GetAuthenticatedUserId();
        int? GetAuthenticatedUserIfExist();
    }
}
