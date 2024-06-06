namespace Domain.Interfaces.Authentication
{
    public interface IPasswordHashService
    {
        string HashPassword(string password);
        bool VerifyPasswordHash(string password, string storedHash);
    }
}
