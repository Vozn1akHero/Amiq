using Amiq.Services.User.Contracts.Auth;
using Amiq.Services.User.Contracts.User;
using Amiq.Services.User.Core.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Amiq.Services.User.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmiqBaseController : ControllerBase
    {
        protected const int CLIENT_CLOSED_REQUEST_STATUS_CODE = 499;

        private ILogger<AmiqBaseController> _logger;

        public AmiqBaseController()
        {
        }

        protected string GetRouteTemplate(string baseName, string rest)
        {
            if (string.IsNullOrEmpty(baseName))
                throw new Exception("Basename cannot be empty");

            const string SEPARATOR = "/";
            StringBuilder routeTemplateBuilder = new StringBuilder();
            routeTemplateBuilder.Append(baseName);
            routeTemplateBuilder.Append(SEPARATOR);
            routeTemplateBuilder.Append(string.IsNullOrEmpty(rest) ? string.Empty : rest);
            return routeTemplateBuilder.ToString();
        }

        //protected DtoJwtStoredUserInfo JwtStoredUserInfo => (DtoJwtStoredUserInfo)HttpContext.Items["user"];

        protected int? JwtStoredUserId
        {
            get {
                string? val = HttpContext.Request.Headers.GetCommaSeparatedValues("Amiq-UserId").FirstOrDefault();
                if (!string.IsNullOrEmpty(val))
                    return int.Parse(val);
                else 
                    return null;
            }
        }

        protected DtoJwtStoredUserInfo? JwtStoredUserInfo
        {
            get
            {
                /*string? token = Request.Cookies["token"];
                if (!string.IsNullOrEmpty(token))
                {
                    return JwtExtensions.GetJwtStoredUserInfo(token);
                }
                return null;*/
                return (DtoJwtStoredUserInfo?)HttpContext.Items["user"];
            }
        }
    }
}
