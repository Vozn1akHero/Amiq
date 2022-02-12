using System;
using System.Collections.Generic;

namespace Amiq.Services.Notification.DataAccessLayer.Models
{
    public partial class GroupVisitation
    {
        public Guid GroupVisitationId { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public DateTime LastVisited { get; set; }
        public long VisitationTotalTime { get; set; }

        public virtual Group Group { get; set; } = null!;
    }
}
