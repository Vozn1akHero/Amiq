using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Workers.Notification
{
    public class NotificationQueueCache
    {
        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        private List<NotificationQueueEntry> cache = new List<NotificationQueueEntry>();

        public void AddToCache(IEnumerable<NotificationQueueEntry> notificationQueueEntries)
        {
            _semaphoreSlim.Wait();

            

            _semaphoreSlim.Release();
        }
    }
}
