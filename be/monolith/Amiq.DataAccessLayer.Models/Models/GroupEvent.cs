using System;
using System.Collections.Generic;

namespace Amiq.DataAccess.Models.Models
{
    public partial class GroupEvent
    {
        public GroupEvent()
        {
            GroupEventParticipants = new HashSet<GroupEventParticipant>();
        }

        public Guid GroupEventId { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string AvatarSrc { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public string Description { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsHidden { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<GroupEventParticipant> GroupEventParticipants { get; set; }
    }
}
