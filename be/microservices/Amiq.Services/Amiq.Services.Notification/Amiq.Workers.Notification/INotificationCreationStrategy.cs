using Amiq.Services.Notification.DataAccessLayer.Models.Models;

namespace Amiq.Workers.Notification
{
    public abstract class NotificationCreationStrategy
    {
        public AmiqNotificationContext DbContext { get; private set; }

        public NotificationCreationStrategy()
        {
            DbContext = new AmiqNotificationContext();
        }

        public abstract IEnumerable<Amiq.Services.Notification.DataAccessLayer.Models.Models.Notification> Create(IEnumerable<UserNotificationsQueue> users);
    }
}
