using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Web.Service.HTTPResult;

namespace Web.Service.Extensions
{
    public static class ControllerBaseExtension
    {
        [NonAction]
        public static InternalServerErrorResult InternalServerError(this ControllerBase controllerBase) => new InternalServerErrorResult();
    }
}
