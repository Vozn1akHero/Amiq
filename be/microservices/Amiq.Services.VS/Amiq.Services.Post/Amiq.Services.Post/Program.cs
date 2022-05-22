using Amiq.ApiGateways.WebApp.Utils;
using Amiq.Services.Post.Mapping;
using Amiq.Services.Post.Messaging;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opts =>
{
    opts.SuppressAsyncSuffixInActionNames = false;
    opts.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
});

builder.Services.AddRouting(opts => opts.LowercaseUrls = true);



AmiqPostAutoMapper.Initialize();

builder.Services.AddHostedService<RabbitMQListener>();

var app = builder.Build();


app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
