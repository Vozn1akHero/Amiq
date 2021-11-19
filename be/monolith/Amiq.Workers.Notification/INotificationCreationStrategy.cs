using Amiq.DataAccess.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Workers.Notification
{
    public abstract class NotificationCreationStrategy
    {
        public AmiqContext DbContext { get; private set; }

        public NotificationCreationStrategy()
        {
            DbContext = new AmiqContext();
        }

        public abstract IEnumerable<DataAccess.Models.Models.Notification> Create(IEnumerable<int> userIds);
    }
}
