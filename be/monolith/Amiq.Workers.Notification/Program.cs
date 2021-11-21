using Amiq.Workers.Notification;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<NotificationWorker>();
    })
    .Build();

await host.RunAsync();
