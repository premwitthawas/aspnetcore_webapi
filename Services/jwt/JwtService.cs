using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using basic.Dtos.jwt;
using basic.Interfaces.jwt;
using Microsoft.IdentityModel.Tokens;

namespace basic.Services.jwt;

public class JwtService : IJwtService
{
    private readonly string _secretKey;
    private readonly string _expDate;

    public JwtService(IConfiguration configuration)
    {
        _secretKey = configuration["Jwt:SecretKey"]!;
        _expDate = configuration["Jwt:ExpirationInMinutes"]!;
    }
    public string GenerateSecurityToken(JwtUserPayload userPayload)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userPayload.Id.ToString()),
                new Claim(ClaimTypes.Name, userPayload.Username),
                new Claim(ClaimTypes.Role, userPayload.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}