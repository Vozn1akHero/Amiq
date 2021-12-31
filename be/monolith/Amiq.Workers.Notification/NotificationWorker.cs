using Amiq.DataAccessLayer.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Amiq.Workers.Notification
{
    public class NotificationWorker : BackgroundService
    {
        private readonly ILogger<NotificationWorker> _logger;
        private readonly AmiqContext _amiqContext;
        private readonly UserPostNotificationCreation _userPostNotificationCreation;
        private readonly GroupPostNotificationCreation _groupPostNotificationCreation;
        private const int TIMEOUT_BETWEEN_BULKS = 5000;
        private const int TAKE_USERS = 50;
        private int _page = 1;

        public NotificationWorker(ILogger<NotificationWorker> logger)
        {
            _logger = logger;
            _amiqContext = new AmiqContext();
            _userPostNotificationCreation = new UserPostNotificationCreation();
            _groupPostNotificationCreation = new GroupPostNotificationCreation();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            //lista id u¿ytkowników
            /*HashSet<int> userIds = _amiqContext.Users.AsNoTracking().Select(e => e.UserId)
                .Take(TAKE_USERS)
                .Skip((_page - 1) * TAKE_USERS)
                .ToHashSet();*/
            HashSet<int> userIds = new HashSet<int> { 6 };

            //wpisy
            var userPostNotifications = _userPostNotificationCreation.Create(userIds);
            var groupPostsNotifications = _groupPostNotificationCreation.Create(userIds);

            //await _amiqContext.Notifications.AddRangeAsync(userPostNotifications);
            //await _amiqContext.Notifications.AddRangeAsync(groupPostsNotifications);
            //await _amiqContext.SaveChangesAsync();

            _page++;

            await Task.Delay(TIMEOUT_BETWEEN_BULKS, stoppingToken);
        }
    }
}