using System.ComponentModel.DataAnnotations;

namespace basic.Models.auth
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters")]
        public required string Username { get; set; }
        [Required]
        [MaxLength(255)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public required string Password { get; set; }
        [Required]
        public Role Role { get; set; } = Role.User;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}