using Amiq.Core.Auth;
using Amiq.Mapping;
using Amiq.WebApi.Core;
using Amiq.WebApi.Middlewares;
using Amiq.WebApi.SignalR;
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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
            services.AddCors();
            
            services.AddControllers(opts =>
            {
                opts.SuppressAsyncSuffixInActionNames = false;
            });
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
                //opts.JsonSerializerOptions.MaxDepth = 0;
                //opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler;
                //opts.JsonSerializerOptions.ReferenceHandler = PreserveReferencesHandling.Objects;
            });
            
            services.AddRouting(options => {
                options.LowercaseUrls = true;
            });
            services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            });

            services.AddSignalR();
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

            app.UseRouting();

            app.UseCors(x => x
                .WithOrigins("http://localhost:3000", "http://localhost:8080")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();

            app.UseAuthorization();

            app.ConfigureMiddlewares();

            app.UseStaticFiles(new StaticFileOptions {
                //HttpsCompression = Microsoft.AspNetCore.Http.Features.HttpsCompressionMode.Compress
            });

            //app.UseResponseWrapper();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("hub/chat");
            });
        }
    }
}
