using System;
using System.Collections.Generic;

namespace Amiq.DataAccess.Models.Models
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

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<GroupEventParticipant> GroupEventParticipants { get; set; }
    }
}
