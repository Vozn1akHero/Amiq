using Amiq.ApiGateways.WebApp.Cache.Redis;
using Amiq.ApiGateways.WebApp.Core;
using Amiq.ApiGateways.WebApp.Core.Auth;
using Amiq.ApiGateways.WebApp.HttpClients;
using Amiq.ApiGateways.WebApp.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

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
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);

                        var ocelotConfigurationPath = Path.Combine(hostingContext.HostingEnvironment.ContentRootPath,
                            "Ocelot", "Configuration");
                        ocelotConfigurationPath = Path.Combine(ocelotConfigurationPath, hostingContext.HostingEnvironment.EnvironmentName);

                        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

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
                        services.AddCors();

                        services.AddControllers(opts =>
                        {
                            opts.SuppressAsyncSuffixInActionNames = false;
                            opts.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
                        });


                        /*services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer("ApiSecurity", options =>
                            {
                                options.RequireHttpsMetadata = false;
                                options.SaveToken = true;
                                options.TokenValidationParameters = JwtExtensions.JwtValidationParameters;
                                options.Events = new JwtBearerEvents
                                {
                                    OnMessageReceived = context =>
                                    {
                                        context.Token = context.Request.Cookies["token"];
                                        return Task.CompletedTask;
                                    },
                                    *//*OnTokenValidated = context => 
                                    {
                                        var claims = new List<Claim>
                                        {
                                            new Claim("ConfidentialAccess", "true")
                                        };
                                        var appIdentity = new ClaimsIdentity(claims);

                                        context.Principal.AddIdentity(appIdentity);

                                        return Task.CompletedTask;
                                    }*//*
                                };
                            });*/
                        services.Configure<JsonOptions>(opts => {
                            opts.SerializerOptions.IgnoreNullValues = true;
                            opts.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                        });

                        services.AddRouting(options => {
                            options.LowercaseUrls = true;
                        });

                        services.AddOcelot()
                            //.AddCacheManager(settings => settings.WithDictionaryHandle())
                            //.AddPolly()
                            ;

                        
                        services.AddSingleton<UserCacheService>();

                        services.AddHttpClient<UserService>();
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();

                        app.UseCors(x => x
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .SetIsOriginAllowed(origin => true)
                            .AllowCredentials());

                        //app.UseAuthentication();
                        //app.UseAuthorization();

                        //app.UseMiddleware<UserRequestContextMiddleware>();
                        //app.UseMiddleware<RequestCancellationMiddleware>();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                        });

                        app.UseOcelot().Wait();
                    })
                    //.UseIIS()
                    //.UseIISIntegration()
                    ;
                });
    }
}
