namespace Amiq.Services.Notification.Messaging.IntegrationEvents
{
    public class GroupParticipantModificationEvent : IntegrationEvent
    {
        public Guid GroupParticipantId { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string Action { get; set; }
    }
}
