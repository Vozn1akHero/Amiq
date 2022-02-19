using Microsoft.AspNetCore.HeaderPropagation;
using Microsoft.Extensions.Primitives;

namespace Amiq.Services.Base.Auth
{
    public static class AuthHeaderPropagation
    {
        public static IServiceCollection AddAuthHeaderPropagation(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHeaderPropagation(options =>
            {
                options.Headers.Add("Cookie", context =>
                {
                    string? accessToken = context.HttpContext.Request.Cookies["token"];
                    return accessToken != null ? new StringValues($"token={accessToken}") : new StringValues();
                });
            });

            return serviceCollection;
        }
    }
}
