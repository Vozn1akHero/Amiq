using Amiq.Core.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amiq.WebApi.Middlewares
{
    public class WorkContextMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<WorkContextMiddleware> _logger;
        public WorkContextMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory?.CreateLogger<WorkContextMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"Request URL: {Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(context.Request)}");

            string token = context.Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token)) {
                var dataFromToken = JwtExtensions.GetJwtStoredUserInfo(token);
                context.Items.Add(new KeyValuePair<object, object>("user", dataFromToken));
            }

            await this._next(context);
        }
    }
}
