using System;
using System.Collections.Generic;

namespace Amiq.Services.Notification.DataAccessLayer.Models.Models
{
    public partial class NotificationRequest
    {
        public Guid NotificationRequestId { get; set; }
        public int UserId { get; set; }
        public DateTime VisitedAt { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
