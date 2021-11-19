using Amiq.DataAccess.Models.Models;

namespace Amiq.Workers.Notification
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly AmiqContext _amiqContext;

        private const int TIMEOUT = 1000;
        private int _page = 1;
        private const int TAKE = 50;

        private const int MOST_VISITED_ENTITIES_COUNT = 5;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _amiqContext = new AmiqContext();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                //lista id u¿ytkowników
                HashSet<int> userIds = _amiqContext.Users.Select(e => e.UserId)
                    .Take(50)
                    .Skip((_page - 1) * 50)
                    .ToHashSet<int>();
                _page++;

                //wpisy w grupie
                foreach(int userId in userIds)
                {
                    List<DataAccess.Models.Models.Notification> notifications = new();
                    
                }
                
                await Task.Delay(TIMEOUT, stoppingToken);
            }
        }
    }
}