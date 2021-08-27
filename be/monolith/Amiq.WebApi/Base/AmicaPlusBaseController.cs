using Amiq.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.WebApi.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmiqBaseController : ControllerBase
    {
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
    }
}
