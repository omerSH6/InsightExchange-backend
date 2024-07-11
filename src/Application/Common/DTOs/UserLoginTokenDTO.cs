namespace Application.Common.DTOs
{
    public class UserLoginTokenDTO
    {
        public required string LoginToken { get; set; }
        public required string UserName { get; set; }
        public required string Role { get; set; }
        public required int UserId { get; set; }
    }
}
