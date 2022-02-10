using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Workers.Notification
{
    public class GroupEventNotificationCreation : NotificationCreationStrategy
    {
        public override IEnumerable<DataAccessLayer.Models.Models.Notification> Create(IEnumerable<UserNotificationsQueue> users)
        {
            throw new NotImplementedException();
        }
    }
}
