using System;
using System.Collections.Generic;

namespace Amiq.Services.Group.DataAccessLayer.Models.Models
{
    public partial class GroupEvent
    {
        public GroupEvent()
        {
            GroupEventParticipants = new HashSet<GroupEventParticipant>();
        }

        public Guid GroupEventId { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
        public string AvatarSrc { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public string? Description { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsHidden { get; set; }

        public virtual User CreatedByNavigation { get; set; } = null!;
        public virtual Group Group { get; set; } = null!;
        public virtual ICollection<GroupEventParticipant> GroupEventParticipants { get; set; }
    }
}
