using Amiq.Services.Base.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Amiq.Services.Base.Controllers
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

        protected int JwtStoredUserId
        {
            get
            {
                if (JwtStoredUserInfo != null)
                {
                    return JwtStoredUserInfo.UserId;
                }
                else throw new Exception($"{nameof(JwtStoredUserId)} is null");
            }
        }

        protected DtoJwtStoredUserInfo? JwtStoredUserInfo
        {
            get
            {
                string? token = Request.Cookies["token"];
                if (!string.IsNullOrEmpty(token))
                {
                    return JwtExtensions.GetJwtStoredUserInfo(token);
                }
                return null;
                //return (DtoJwtStoredUserInfo?)HttpContext.Items["user"];
            }
        }
    }
}
