using basic.Models.auth;

namespace basic.Dtos.auth;

public record UserResDto(Guid Id);
public record UserGetResDto(Guid Id, string Username,Role Role);