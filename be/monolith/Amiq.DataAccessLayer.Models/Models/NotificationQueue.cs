using System;
using System.Collections.Generic;

namespace Amiq.DataAccess.Models.Models
{
    public partial class NotificationQueue
    {
        public Guid NotificationQueueId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; }
    }
}
