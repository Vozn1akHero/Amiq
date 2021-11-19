using Amiq.Contracts.User;
using System;

namespace Amiq.Contracts.Notification
{
    public class DtoNotification
    {
        public Guid NotificationId { get; set; }
        public DtoBasicUserInfo User { get; set; }
        public string Text { get; set; }
        public string ImageSrc { get; set; }
        public string Link { get; set; }
        public DateTime CreatedAt { get; set; }
        public DtoNotificationType NotificationType { get; set; }
    }
}
