namespace Application.DTOs
{
    public class LoginDTO
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
    }
}
