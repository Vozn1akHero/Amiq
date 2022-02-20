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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<UserCacheService>();

builder.Services.AddHostedService<RabbitMQListener>();

AmiqFriendshipAutoMapper.Initialize();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHeaderPropagation();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.Run();
