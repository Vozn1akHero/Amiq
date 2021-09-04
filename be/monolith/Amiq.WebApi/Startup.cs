using Amiq.Core.Auth;
using Amiq.Mapping;
using Amiq.WebApi.Core;
using Amiq.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            APAutoMapper.Initialize();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Amiq", Version = "v1" });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
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
                        }
                    };
                });
            services.Configure<JsonOptions>(opts => {
                opts.JsonSerializerOptions.IgnoreNullValues = true;
                opts.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            });
            services.AddRouting(options => {
                options.LowercaseUrls = true;
            });
            services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            });

            //services.ConfigureMapper();

            services.ConfigureCustomServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Amiq v1"));
            }

            //app.UseHttpsRedirection();
            app.UseCors(e => e
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.ConfigureMiddlewares();

            app.UseStaticFiles(new StaticFileOptions {
                HttpsCompression = Microsoft.AspNetCore.Http.Features.HttpsCompressionMode.Compress
            });

            //app.UseResponseWrapper();

            

            app.Use(async (http, next) =>
            {
                //remember previous body
                var currentBody = http.Response.Body;

                using (var memoryStream = new MemoryStream())
                {
                    //set the current response to the memorystream.
                    http.Response.Body = memoryStream;

                    await next();

                    string requestId = Guid.NewGuid().ToString();

                    //reset the body as it gets replace due to https://github.com/aspnet/KestrelHttpServer/issues/940
                    http.Response.Body = currentBody;
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    //build our content wrappter.
                    var content = new StringBuilder();
                    content.AppendLine("{");
                    content.AppendLine("  \"RequestId\":\"" + requestId + "\",");
                    content.AppendLine("  \"StatusCode\":" + http.Response.StatusCode + ",");
                    content.AppendLine("  \"Result\":");
                    //add the original content.
                    content.AppendLine(new StreamReader(memoryStream).ReadToEnd());
                    content.AppendLine("}");

                    await http.Response.WriteAsync(content.ToString());

                }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
