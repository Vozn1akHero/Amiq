using System;
using System.Collections.Generic;

namespace Amiq.Services.Group.DataAccessLayer.Models
{
    public partial class Group
    {
        public Group()
        {
            GroupBlockedUsers = new HashSet<GroupBlockedUser>();
            GroupDescriptionBlocks = new HashSet<GroupDescriptionBlock>();
            GroupEvents = new HashSet<GroupEvent>();
            GroupParticipants = new HashSet<GroupParticipant>();
            HiddenGroups = new HashSet<HiddenGroup>();
        }

        public int GroupId { get; set; }
        public string Name { get; set; } = null!;
        public string AvatarSrc { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; } = null!;
        public virtual ICollection<GroupBlockedUser> GroupBlockedUsers { get; set; }
        public virtual ICollection<GroupDescriptionBlock> GroupDescriptionBlocks { get; set; }
        public virtual ICollection<GroupEvent> GroupEvents { get; set; }
        public virtual ICollection<GroupParticipant> GroupParticipants { get; set; }
        public virtual ICollection<HiddenGroup> HiddenGroups { get; set; }
    }
}
