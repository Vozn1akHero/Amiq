using Amiq.ApiGateways.WebApp.Utils;
using Amiq.Services.User.HttpClients;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.HeaderPropagation;

var builder = WebApplication.CreateBuilder(args);

/*builder.Host.ConfigureAppConfiguration((context, config) =>
{
    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    config.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
});*/
//builder.WebHost.UseIISIntegration();

//builder.Services.AddCors();
builder.Services.AddHeaderPropagation(options =>
{
    options.Headers.Add("Cookie", context =>
    {
        string? accessToken = context.HttpContext.Request.Cookies["token"];
        return accessToken != null ? new StringValues($"token={accessToken}") : new StringValues();
    });
});

builder.Services.AddHttpClient<FriendshipService>().AddHeaderPropagation(options =>
{
    options.Headers.Add("Cookie");
});

builder.Services.AddControllers(opts =>
{
    opts.SuppressAsyncSuffixInActionNames = false;
    opts.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
});

/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                //options.RequireHttpsMetadata = false;
                                //options.SaveToken = true;
                                //options.TokenValidationParameters = JwtExtensions.JwtValidationParameters;
                                options.Events = new JwtBearerEvents
                                {
                                    OnMessageReceived = context =>
                                    {
                                        context.Token = context.Request.Cookies["token"];
                                        return Task.CompletedTask;
                                    }
                                };
                            });*/

//builder.Services.AddMvc();

builder.Services.AddRouting(opts => opts.LowercaseUrls = true);

//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/*app.Use(async (context, next) =>
{
    var token = context.Request.Cookies["token"];
    if (!string.IsNullOrEmpty(token))
    {
        context.Request.Headers.Add("Authorization", "Bearer " + token);
    }
    await next();
});*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseHeaderPropagation();

app.UseRouting();

/*app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());*/

//app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
