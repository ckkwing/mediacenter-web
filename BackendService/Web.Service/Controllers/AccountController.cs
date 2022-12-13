using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Web.Service.DataAccess.Entity;
using Web.Service.DataAccess.Interface;
using Web.Service.Extensions;
using Web.Service.RequestModel;
using Web.Service.ResponseModel;
using Web.Service.Security;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IUserDao _userDao;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(IUserDao userDao, UserManager<User> userManager, IConfiguration configuration)
        {
            _userDao = userDao;
            _userManager = userManager;
            _configuration = configuration;
        }   

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestModel loginEntity)
        {
            if (loginEntity.UserName.IsNullOrEmpty() || loginEntity.Password.IsNullOrEmpty())
                return BadRequest();

            var existUser = await _userManager.FindByNameAsync(loginEntity.UserName);
            if (null == existUser)
                return Unauthorized("User not found");

            if (!(await _userManager.CheckPasswordAsync(existUser, loginEntity.Password)))
                return Unauthorized("Password incorrect");

            var userRoles = await _userManager.GetRolesAsync(existUser);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, existUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, existUser.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            TokenHelper tokenHelper = new(_configuration);
            var token = tokenHelper.GenerateAccessToken(authClaims.ToArray());


            //return Ok(new TokenEntity() { Token = token.GetString(), Expiration = token.ValidTo });
            return Ok(new LoginResponseModel(){
                Token = new TokenEntity() { Token = token.GetString(), Expiration = token.ValidTo },
                User = existUser.CreateUserWithoutPrivateInfo()
            });
        }

        //[HttpPost("Login1")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login1Async([FromForm] string name, [FromForm] string password)
        //{
        //    if (name.IsNullOrEmpty() || password.IsNullOrEmpty())
        //        return BadRequest();

        //    var existUser = await _userManager.FindByNameAsync(name);

        //    if (existUser != null && await _userManager.CheckPasswordAsync(existUser, password))
        //    {
        //        return Ok();
        //    }

        //    return Unauthorized();
        //}

        [HttpGet("test")]
        public IActionResult test()
        {
            return Ok("test");
        }

    }
}
