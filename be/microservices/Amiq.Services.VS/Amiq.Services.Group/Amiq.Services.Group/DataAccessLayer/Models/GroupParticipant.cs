using System;
using System.Collections.Generic;

namespace Amiq.Services.Group.DataAccessLayer.Models
{
    public partial class GroupParticipant
    {
        public GroupParticipant()
        {
            GroupEventParticipants = new HashSet<GroupEventParticipant>();
        }

        public Guid GroupParticipantId { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public DateTime Joined { get; set; }
        public bool IsAdmin { get; set; }
        public bool? IsParticipantVisible { get; set; }

        public virtual Group Group { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<GroupEventParticipant> GroupEventParticipants { get; set; }
    }
}
