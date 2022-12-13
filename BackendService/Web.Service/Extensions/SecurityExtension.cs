using System.IdentityModel.Tokens.Jwt;

namespace Web.Service.Extensions
{
    public static class SecurityExtension
    {
        public static string GetString(this JwtSecurityToken jwtSecurityToken)
        {
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
