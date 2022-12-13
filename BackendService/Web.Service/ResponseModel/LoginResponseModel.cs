using IdentityModel.Client;
using Web.Service.DataAccess.Entity;
using Web.Service.Security;

namespace Web.Service.ResponseModel
{
    public class LoginResponseModel
    {
        public TokenEntity Token { get; set; } = new TokenEntity();
        public User User { get; set; } = new User();
    }
}
