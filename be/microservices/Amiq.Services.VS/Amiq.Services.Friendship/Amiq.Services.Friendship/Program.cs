using Amiq.ApiGateways.WebApp.Utils;
using Amiq.Services.Base.Auth;
using Amiq.Services.Friendship.Cache.Redis;
using Amiq.Services.Friendship.HttpClients;
using Amiq.Services.Friendship.Mapping;
using Amiq.Services.Friendship.Messaging;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthHeaderPropagation();

builder.Services.AddHeaderPropagation(options =>
{
    options.Headers.Add("Cookie", context =>
    {
        string? accessToken = context.HttpContext.Request.Cookies["token"];
        return accessToken != null ? new StringValues($"token={accessToken}") : new StringValues();
    });
});

builder.Services.AddHttpClient<UserService>().AddHeaderPropagation(options =>
{
    options.Headers.Add("Cookie");
});

builder.Services.AddControllers(opts =>
{
    opts.SuppressAsyncSuffixInActionNames = false;
    opts.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
});

//builder.Services.AddSingleton<UserCacheService>();

builder.Services.AddHostedService<RabbitMQListener>();

AmiqFriendshipAutoMapper.Initialize();

var app = builder.Build();



app.UseHeaderPropagation();

app.UseRouting();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
