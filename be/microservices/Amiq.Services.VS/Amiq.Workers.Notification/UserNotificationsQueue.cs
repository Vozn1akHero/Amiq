using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Workers.Notification
{
    public class UserNotificationsQueue
    {
        public int UserId { get; set; }
        public Guid NotificationGroupId { get; set; }
    }
}
