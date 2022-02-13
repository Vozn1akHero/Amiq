using Amiq.Workers.Notification.Models;

namespace Amiq.Workers.Notification
{
    public abstract class NotificationCreationStrategy
    {
        public AmiqNotificationWorkerContext DbContext { get; private set; }

        public NotificationCreationStrategy()
        {
            DbContext = new AmiqNotificationWorkerContext();
        }

        public abstract IEnumerable<Models.Notification> Create(IEnumerable<UserNotificationsQueue> users);
    }
}
