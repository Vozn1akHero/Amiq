using System;
using System.Collections.Generic;

namespace Amiq.DataAccess.Models.Models
{
    public partial class Group
    {
        public Group()
        {
            GroupBlockedUsers = new HashSet<GroupBlockedUser>();
            GroupDescriptionBlocks = new HashSet<GroupDescriptionBlock>();
            GroupEvents = new HashSet<GroupEvent>();
            GroupParticipants = new HashSet<GroupParticipant>();
            GroupPostComments = new HashSet<GroupPostComment>();
            GroupPosts = new HashSet<GroupPost>();
            GroupVisitations = new HashSet<GroupVisitation>();
            HiddenGroups = new HashSet<HiddenGroup>();
        }

        public int GroupId { get; set; }
        public string Name { get; set; }
        public string AvatarSrc { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual ICollection<GroupBlockedUser> GroupBlockedUsers { get; set; }
        public virtual ICollection<GroupDescriptionBlock> GroupDescriptionBlocks { get; set; }
        public virtual ICollection<GroupEvent> GroupEvents { get; set; }
        public virtual ICollection<GroupParticipant> GroupParticipants { get; set; }
        public virtual ICollection<GroupPostComment> GroupPostComments { get; set; }
        public virtual ICollection<GroupPost> GroupPosts { get; set; }
        public virtual ICollection<GroupVisitation> GroupVisitations { get; set; }
        public virtual ICollection<HiddenGroup> HiddenGroups { get; set; }
    }
}
