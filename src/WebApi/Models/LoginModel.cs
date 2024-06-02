using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class LoginModel
    {
        public required string Username { get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
