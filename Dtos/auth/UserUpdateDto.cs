using System.ComponentModel.DataAnnotations;

namespace basic.Dtos.auth;
public record UserUpdateDto([Required] string Username);