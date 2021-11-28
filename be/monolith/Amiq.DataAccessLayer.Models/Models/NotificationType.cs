using System;
using System.Collections.Generic;

namespace Amiq.DataAccessLayer.Models.Models
{
    public partial class NotificationType
    {
        public NotificationType()
        {
            Notifications = new HashSet<Notification>();
        }

        public int NotificationTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
