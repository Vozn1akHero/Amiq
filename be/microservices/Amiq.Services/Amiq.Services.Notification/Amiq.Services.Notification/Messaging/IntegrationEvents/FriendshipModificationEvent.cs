namespace Amiq.Services.Notification.Messaging.IntegrationEvents
{
    public class FriendshipModificationEvent : IntegrationEvent
    {
        public Guid FriendshipId { get; set; }
        public int FirstUserId { get; set; }
        public int SecondUserId { get; set; }
        public string Action { get; set; }
    }
}
