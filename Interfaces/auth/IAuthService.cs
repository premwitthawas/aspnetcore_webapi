using basic.Dtos.auth;
using System;
namespace basic.Interfaces.auth;

public interface IAuthService
{
    Task<UserResDto> Register(UserCreateDto user);
    Task<string> Login(UserLoginDto user);
    Task<UserGetResDto> GetUserByid(Guid id);
    Task<UserResDto> UpdateUser(Guid id,UserUpdateDto user);
    Task<bool> DeleteUser(Guid id);
}
