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
