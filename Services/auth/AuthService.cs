using basic.Data;
using basic.Dtos.auth;
using basic.Dtos.jwt;
using basic.Interfaces.auth;
using basic.Interfaces.jwt;
using basic.Models.auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace basic.Services.auth;

public class AuthService : IAuthService
{
    private readonly AuthDbContext _authDb;
    private readonly IJwtService _jwtService;

    public AuthService(AuthDbContext authDb, IJwtService jwtService)
    {
        _authDb = authDb;
        _jwtService = jwtService;
    }
    public async Task<bool> DeleteUser(Guid id)
    {
        var user = await _authDb.users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        _authDb.users.Remove(user);
        await _authDb.SaveChangesAsync();
        return true;
    }

    public async Task<UserGetResDto> GetUserByid(Guid id)
    {
        var user = await _authDb.users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        var userDto = new UserGetResDto(user.Id, user.Username, user.Role);
        return userDto;
    }

    public async Task<string> Login(UserLoginDto userLoginDto)
    {
        var user = await _authDb.users.FirstOrDefaultAsync(x => x.Username == userLoginDto.Username);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        bool validPassword = BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.Password);
        if (!validPassword)
        {
            throw new Exception("Invalid password");
        }
        var jwtUserPayload = new JwtUserPayload
        {
            Id = user.Id,
            Username = user.Username,
            Role = user.Role
        };
        var token = _jwtService.GenerateSecurityToken(jwtUserPayload);
        return token;
    }

    public async Task<UserResDto> Register(UserCreateDto userCreateDTO)
    {
        var existingUser = _authDb.users.FirstOrDefault(x => x.Username == userCreateDTO.Username);
        if (existingUser != null)
        {
            throw new Exception("User already exists");
        }
        var passwordHashed = BCrypt.Net.BCrypt.HashPassword(userCreateDTO.Password, 10);
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = userCreateDTO.Username,
            Password = passwordHashed,
        };
        _authDb.users.Add(user);
        await _authDb.SaveChangesAsync();
        var res = new UserResDto(user.Id);
        return res;
    }
    public async Task<UserResDto> UpdateUser(Guid id, UserUpdateDto userUpdateDto)
    {
        var user = await _authDb.users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        user.Username = userUpdateDto.Username;
        await _authDb.SaveChangesAsync();
        return new UserResDto(user.Id);
    }
};