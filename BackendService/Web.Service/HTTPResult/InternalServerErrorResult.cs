using Microsoft.AspNetCore.Mvc;

namespace Web.Service.HTTPResult
{
    public class InternalServerErrorResult : StatusCodeResult
    {
        private const int DefaultStatusCode = StatusCodes.Status500InternalServerError;

        public InternalServerErrorResult()
            :base(DefaultStatusCode)
        {

        }
    }
}
