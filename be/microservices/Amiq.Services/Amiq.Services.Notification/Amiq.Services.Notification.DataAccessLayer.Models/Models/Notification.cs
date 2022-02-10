using System;
using System.Collections.Generic;

namespace Amiq.Services.Notification.DataAccessLayer.Models.Models
{
    public partial class Notification
    {
        public Guid NotificationId { get; set; }
        public Guid NotificationGroupId { get; set; }
        public string ImageSrc { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string? Link { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public string NotificationType { get; set; } = null!;
        public bool IsRead { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
