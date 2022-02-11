namespace Amiq.Services.Notification.Messaging.IntegrationEvents
{
    public class GroupPostModificationEvent : IntegrationEvent
    {
        public Guid GroupPostId { get; set; }
        public Guid PostId { get; set; }
        public string TextContent { get; set; }
        public int? EditedBy { get; set; }
        public DateTime? EditedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int GroupId { get; set; }
        public int AuthorId { get; set; }
        public bool VisibleAsCreatedByAdmin { get; set; }
        public string Action { get; set; }
    }
}
