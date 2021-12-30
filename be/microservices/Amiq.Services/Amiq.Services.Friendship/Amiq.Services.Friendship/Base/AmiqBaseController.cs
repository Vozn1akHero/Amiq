using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.Friendship.Base
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

        protected int JwtStoredUserId => int.Parse(HttpContext.Request.Headers["Amiq-UserId"]);
    }
}
