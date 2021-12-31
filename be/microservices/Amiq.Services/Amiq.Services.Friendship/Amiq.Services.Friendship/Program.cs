using Amiq.Services.Friendship.Amqp;
using Amiq.Services.Friendship.Cache.Redis;
using Amiq.Services.Friendship.HttpClients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<UserCacheService>();
builder.Services.AddHttpClient<UserService>();

builder.Services.AddHostedService<RabbitMQListener>();

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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
