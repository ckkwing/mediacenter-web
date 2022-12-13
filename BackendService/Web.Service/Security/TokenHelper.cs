using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Web.Service.Security
{
    public class TokenHelper
    {
        private readonly IConfiguration _configuration;
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly SymmetricSecurityKey _symmetricKey;

        public TokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            var jwtSection = _configuration.GetSection("Jwt");
            _secretKey = jwtSection.GetValue<string>("SecretKey");
            _issuer = jwtSection.GetValue<string>("Issuer");
            _audience = jwtSection.GetValue<string>("Audience");
            _symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        }

        public JwtSecurityToken GenerateAccessToken(Claim[] claims)
        {

            return new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(_symmetricKey, SecurityAlgorithms.HmacSha256)
                );
        }
    }
}
