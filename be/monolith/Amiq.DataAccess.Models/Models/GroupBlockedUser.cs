using System;
using System.Collections.Generic;

#nullable disable

namespace Amiq.DataAccess.Models.Models
{
    public partial class GroupBlockedUser
    {
        public Guid GroupBlockedUserId { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public DateTime BannedAt { get; set; }
        public DateTime? BannedUntil { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
