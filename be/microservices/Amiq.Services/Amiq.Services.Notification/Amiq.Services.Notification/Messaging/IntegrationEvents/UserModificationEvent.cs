namespace Amiq.Services.Notification.Messaging.IntegrationEvents
{
    public class UserModificationEvent : IntegrationEvent
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AvatarPath { get; set; }

    }
}
