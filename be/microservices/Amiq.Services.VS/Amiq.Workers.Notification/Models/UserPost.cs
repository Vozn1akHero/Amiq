using System;
using System.Collections.Generic;

namespace Amiq.Workers.Notification.Models
{
    public partial class UserPost
    {
        public Guid UserPostId { get; set; }
        public Guid PostId { get; set; }
        public string? TextContent { get; set; }
        public int? EditedBy { get; set; }
        public DateTime? EditedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
