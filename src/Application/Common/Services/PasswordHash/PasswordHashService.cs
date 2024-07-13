using System.Security.Cryptography;
using System.Text;
using Application.Common.Services.PasswordHash.Interfaces;

namespace Application.Common.Services.PasswordHash
{
    public class PasswordHashService : IPasswordHashService
    {
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public bool VerifyPasswordHash(string password, string storedHash)
        {
            var hash = HashPassword(password);
            return hash == storedHash;
        }
    }
}
