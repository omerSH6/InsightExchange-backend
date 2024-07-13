namespace Application.Common.Services.PasswordHash.Interfaces
{
    public interface IPasswordHashService
    {
        string HashPassword(string password);
        bool VerifyPasswordHash(string password, string storedHash);
    }
}
