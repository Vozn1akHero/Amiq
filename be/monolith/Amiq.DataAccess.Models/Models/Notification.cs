using System;
using System.Collections.Generic;

namespace Amiq.DataAccess.Models.Models
{
    public partial class Notification
    {
        public Guid NotificationId { get; set; }
        public int? NotificationTypeId { get; set; }
        public string ImageSrc { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual NotificationType NotificationType { get; set; }
    }
}
