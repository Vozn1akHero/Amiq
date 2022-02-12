namespace Amiq.Services.Post.Messaging.IntegrationEvents
{
    public class UserPostModificationEvent : IntegrationEvent
    {
        public UserPostModificationEvent(Guid userPostId, Guid postId, string textContent, int? editedBy, DateTime? editedAt, DateTime createdAt, int userId, string action)
        {
            UserPostId = userPostId;
            PostId = postId;
            TextContent = textContent;
            EditedBy = editedBy;
            EditedAt = editedAt;
            CreatedAt = createdAt;
            UserId = userId;
            Action = action;
        }

        public Guid UserPostId { get; set; }
        public Guid PostId { get; set; }
        public string TextContent { get; set; }
        public int? EditedBy { get; set; }
        public DateTime? EditedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
    }
}
