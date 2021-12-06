using Amiq.ApiGateways.WebApp.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ocelot.Cache.CacheManager;

namespace Amiq.ApiGateways.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config
                            .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                            .AddJsonFile("appsettings.json", true, true)
                            .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);

                        var ocelotConfigurationPath = Path.Combine(hostingContext.HostingEnvironment.ContentRootPath,
                            "Ocelot", "Configuration");
                        ocelotConfigurationPath = Path.Combine(ocelotConfigurationPath, hostingContext.HostingEnvironment.EnvironmentName);

                        config.AddOcelot(ocelotConfigurationPath, hostingContext.HostingEnvironment);

                        //config.AddJsonFile("ocelot.json", false, true);

                        config.AddEnvironmentVariables();

                        //config.Build();

                        /*if (hostingContext.HostingEnvironment.IsDevelopment())
                        {
                            config
                                .AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", false, true);
                        }
                        else
                        {
                            config
                                .AddJsonFile("ocelot.json", false, true);
                        }*/
                    })//.UseStartup<Startup>();
                    .ConfigureServices(services =>
                    {
                        services.AddOcelot()
                            .AddCacheManager(settings => settings.WithDictionaryHandle());
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGet("/", async context =>
                            {
                                await context.Response.WriteAsync("Hello World!");
                            });
                        });

                        app.UseOcelot().Wait();

                        //app.UseMiddleware<UserRequestContextMiddleware>();
                    })
                    .UseIIS()
                    .UseIISIntegration();
                });
    }
}
