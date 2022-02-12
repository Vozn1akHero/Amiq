using Amiq.Services.Notification.DataAccessLayer.Models.Models;

namespace Amiq.Workers.Notification
{
    public class NotificationWorker : IHostedService, IDisposable //BackgroundService
    {
        private readonly ILogger<NotificationWorker> _logger;
        private readonly AmiqNotificationContext _amiqContext;
        private readonly UserPostNotificationCreation _userPostNotificationCreation;
        private readonly GroupPostNotificationCreation _groupPostNotificationCreation;
        private const int TIMEOUT_BETWEEN_BULKS = 5000;
        private const int TAKE_USERS = 50;
        private int _page = 1;
        private Timer? _timer;

        public NotificationWorker(ILogger<NotificationWorker> logger)
        {
            _logger = logger;
            _amiqContext = new AmiqNotificationContext();
            _userPostNotificationCreation = new UserPostNotificationCreation();
            _groupPostNotificationCreation = new GroupPostNotificationCreation();
        }

        /*protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(1));

            //await Task.Delay(TIMEOUT_BETWEEN_BULKS, stoppingToken);
        }*/

        public Task StartAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            //_timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(1));

            DoWork(null);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            //lista id u¿ytkowników
            /*HashSet<int> userIds = _amiqContext.Users.AsNoTracking().Select(e => e.UserId)
                .Take(TAKE_USERS)
                .Skip((_page - 1) * TAKE_USERS)
                .ToHashSet();*/
            //HashSet<int> userIds = new HashSet<int> { 6 };
            var users = new List<UserNotificationsQueue> {
                new UserNotificationsQueue {
                    UserId = 6,
                    NotificationGroupId = Guid.NewGuid()
                }
            };

            //wpisy
            var userPostNotifications = _userPostNotificationCreation.Create(users);
            var groupPostsNotifications = _groupPostNotificationCreation.Create(users);

            _amiqContext.Notifications.AddRange(userPostNotifications);
            _amiqContext.Notifications.AddRange(groupPostsNotifications);

            _amiqContext.SaveChanges();

            _page++;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}