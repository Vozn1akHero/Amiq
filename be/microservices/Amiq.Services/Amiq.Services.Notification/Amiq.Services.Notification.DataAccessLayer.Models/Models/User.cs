using System;
using System.Collections.Generic;

namespace Amiq.Services.Notification.DataAccessLayer.Models.Models
{
    public partial class User
    {
        public User()
        {
            NotificationQueues = new HashSet<NotificationQueue>();
            NotificationRequests = new HashSet<NotificationRequest>();
            Notifications = new HashSet<Notification>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string AvatarPath { get; set; } = null!;

        public virtual ICollection<NotificationQueue> NotificationQueues { get; set; }
        public virtual ICollection<NotificationRequest> NotificationRequests { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
