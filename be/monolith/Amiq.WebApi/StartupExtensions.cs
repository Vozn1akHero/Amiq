using Amiq.WebApi.Core.Files;
using Amiq.WebApi.Middlewares;
using Amiq.WebApi.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Amiq.WebApi
{
    internal static class StartupExtensions
    {
        public static void ConfigureMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestContextMiddleware>();
        }

        public static void ConfigureCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ISignalRChatService, SignalRChatService>();
            services.AddSingleton<ILocalFileStorage, LocalFilesStorage>();
        }
    }
}
