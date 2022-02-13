using Amiq.Static.Core.Files;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ILocalFileStorage, LocalFilesStorage>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseStaticFiles(new StaticFileOptions
{
    
});

app.MapPost("/upload", async ([FromBodyAttribute] IFormFile file) =>
{
    var localFileStorage = app.Services.GetService<ILocalFileStorage>();
    var uploadResult = await localFileStorage.UploadFileAsync(file);
    return new OkResult();
});

app.Run();
