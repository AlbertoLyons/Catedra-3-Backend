using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Catedra_3_Backend.src.models;

namespace Catedra_3_Backend.src.interfaces
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _userManager = userManager;
            var signinKey = _config["JWT_SIGNINKEY"];
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey!));
        }
        public async Task<string> CreateToken(User user)
        {
            // Crear los claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
            };
            // Agregar los roles
            var roles = await _userManager.GetRolesAsync(user);
            // Agregar los roles como claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            // Crear las credenciales
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            // Crear el descriptor del token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
                Issuer = _config["JWT_IUSSER"],
                Audience = _config["JWT_AUDIENCE"]
            };
            // Crear el token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}