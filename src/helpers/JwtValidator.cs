using System.IdentityModel.Tokens.Jwt;

namespace Catedra_3_Backend.src.helpers
{
    public class JwtValidator
    {
        public bool IsTokenExpired(string token)
        {
            var jwtToken = new JwtSecurityToken(token);
            return jwtToken.ValidTo < DateTime.UtcNow;
        }
    }
}