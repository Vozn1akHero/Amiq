using Amiq.ApiGateways.WebApp.Utils;
using Amiq.Services.Friendship.Cache.Redis;
using Amiq.Services.Friendship.HttpClients;
using Amiq.Services.Friendship.Mapping;
using Amiq.Services.Friendship.Messaging;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opts =>
{
    opts.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<UserCacheService>();
builder.Services.AddHttpClient<UserService>();

builder.Services.AddHostedService<RabbitMQListener>();

AmiqFriendshipAutoMapper.Initialize();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.Run();
