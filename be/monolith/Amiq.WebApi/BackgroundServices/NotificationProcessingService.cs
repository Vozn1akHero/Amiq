using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Amiq.WebApi.BackgroundServices
{
    public class NotificationProcessingService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
