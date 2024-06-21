using System.ComponentModel.DataAnnotations;

namespace basic.Dtos.auth
{
    public class UserLoginDto
    {
        [Required]
        [MinLength(3,ErrorMessage = "Username must be at least 3 characters long")]
        public string Username { get; set; } = string.Empty;
        [Required]
        [MinLength(6,ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = string.Empty;
    };
}