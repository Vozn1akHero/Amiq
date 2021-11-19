using System;
using System.Collections.Generic;

namespace Amiq.DataAccess.Models.Models
{
    public partial class ProfileVisitation
    {
        public Guid ProfileVisitationId { get; set; }
        public int UserId { get; set; }
        public int VisitedUserId { get; set; }
        public DateTime LastVisited { get; set; }
        public long VisitationTotalTime { get; set; }
    }
}
