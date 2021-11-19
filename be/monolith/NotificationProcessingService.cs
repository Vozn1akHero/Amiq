using Amiq.DataAccess.Models.Models;
using Microsoft.Extensions.Hosting;

namespace Amiq.BackgroundServices
{
    public class NotificationProcessingService : BackgroundService
    {
        private const int TIMEOUT = 10;
        private const int BULK_COUNT = 10;

        private AmiqContext _amiqContext = new AmiqContext();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            /*while (!stoppingToken.IsCancellationRequested)
            {
                var queue = await _amiqContext.NotificationQueues.T
            }*/

            //return Task.CompletedTask;
        }
    }
}