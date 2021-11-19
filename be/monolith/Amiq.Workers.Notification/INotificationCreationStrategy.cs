using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Workers.Notification
{
    public interface INotificationCreationStrategy
    {
        IEnumerable<DataAccess.Models.Models.Notification> Create(IEnumerable<int> userIds);
    }
}
