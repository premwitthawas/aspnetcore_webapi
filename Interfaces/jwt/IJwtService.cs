using basic.Dtos.jwt;

namespace basic.Interfaces.jwt
{
    public interface IJwtService
    {
        string GenerateSecurityToken(JwtUserPayload userPayload);
    }
}