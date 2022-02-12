namespace Amiq.Services.Post.Messaging.IntegrationEvents
{
    public class GroupPostModificationEvent : IntegrationEvent
    {
        public GroupPostModificationEvent(Guid groupPostId, Guid postId, string textContent, int? editedBy, DateTime? editedAt, DateTime createdAt, int groupId, int authorId, bool visibleAsCreatedByAdmin, string action)
        {
            GroupPostId = groupPostId;
            PostId = postId;
            TextContent = textContent;
            EditedBy = editedBy;
            EditedAt = editedAt;
            CreatedAt = createdAt;
            GroupId = groupId;
            AuthorId = authorId;
            VisibleAsCreatedByAdmin = visibleAsCreatedByAdmin;
            Action = action;
        }

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
