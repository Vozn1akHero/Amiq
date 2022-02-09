using System;
using System.Collections.Generic;

namespace Amiq.Services.Notification.DataAccessLayer.Models.Models
{
    public partial class NotificationQueue
    {
        public Guid NotificationQueueId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
