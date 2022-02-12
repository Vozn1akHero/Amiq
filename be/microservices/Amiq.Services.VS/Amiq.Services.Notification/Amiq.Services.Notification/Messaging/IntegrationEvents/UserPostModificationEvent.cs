namespace Amiq.Services.Notification.Messaging.IntegrationEvents
{
    public class UserPostModificationEvent : IntegrationEvent
    {
        public Guid UserPostId { get; set; }
        public Guid PostId { get; set; }
        public string TextContent { get; set; }
        public int EditedBy { get; set; }
        public DateTime? EditedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
    }
}
