using basic.Models.auth;

namespace basic.Dtos.auth;
public record UserDto(Guid Id,string Username,string Password,Role Role,DateTime CreatedAt,DateTime UpdatedAt);