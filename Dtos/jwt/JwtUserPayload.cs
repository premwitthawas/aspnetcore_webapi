using basic.Models.auth;

namespace basic.Dtos.jwt
{
    public class JwtUserPayload
    {

        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public Role Role { get; set; } = Role.User;

    }
}