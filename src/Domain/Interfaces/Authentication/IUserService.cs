namespace Domain.Interfaces.Authentication
{
    public interface IUserService
    {
        int GetAuthenticatedUserId();
        int? GetAuthenticatedUserIfExist();
    }
}
