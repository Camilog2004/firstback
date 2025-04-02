using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using firstback.user;
using Microsoft.IdentityModel.Tokens;

namespace firstback.JWT
{
    public class AuthService
    {
        private readonly IConfiguration _config;
        public AuthService(IConfiguration config) => _config = config;

        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, user.RoleId.ToString() ?? string.Empty),
            };
            var jwtKey = _config["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key", "JWT key cannot be null");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}