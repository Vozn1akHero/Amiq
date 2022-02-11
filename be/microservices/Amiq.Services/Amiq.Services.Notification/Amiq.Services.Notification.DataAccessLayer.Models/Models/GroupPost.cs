using System;
using System.Collections.Generic;

namespace Amiq.Services.Notification.DataAccessLayer.Models.Models
{
    public partial class GroupPost
    {
        public Guid GroupPostId { get; set; }
        public Guid PostId { get; set; }
        public string? TextContent { get; set; }
        public int? EditedBy { get; set; }
        public DateTime? EditedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int GroupId { get; set; }
        public int AuthorId { get; set; }
        public bool VisibleAsCreatedByAdmin { get; set; }

        public virtual User Author { get; set; } = null!;
        public virtual User? EditedByNavigation { get; set; }
        public virtual Group Group { get; set; } = null!;
    }
}
