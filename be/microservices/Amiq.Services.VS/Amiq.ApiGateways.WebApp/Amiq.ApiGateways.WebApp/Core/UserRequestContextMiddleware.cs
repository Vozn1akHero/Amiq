using Amiq.ApiGateways.WebApp.Core.Auth;
using Amiq.Services.Base.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Amiq.ApiGateways.WebApp.Core
{
    public class UserRequestContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UserRequestContextMiddleware> _logger;
        public UserRequestContextMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory?.CreateLogger<UserRequestContextMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"Request URL: {Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(context.Request)}");

            string token = context.Request.Cookies["token"];

            if (!string.IsNullOrEmpty(token))
            {
                if (JwtExtensions.ValidateToken(token))
                {
                    var dataFromToken = JwtExtensions.GetJwtStoredUserInfo(token);
                    context.Request.Headers.Add("Amiq-UserId", Convert.ToString(dataFromToken.UserId));
                    //context.Request.Headers.Add("Amiq-UserId", Convert.ToString(dataFromToken.UserId));
                }
            }

            await _next(context);
        }
    }
}
