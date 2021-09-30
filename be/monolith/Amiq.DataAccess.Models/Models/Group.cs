using System;
using System.Collections.Generic;

#nullable disable

namespace Amiq.DataAccess.Models.Models
{
    public partial class Group
    {
        public Group()
        {
            GroupBlockedUsers = new HashSet<GroupBlockedUser>();
            GroupDescriptionBlocks = new HashSet<GroupDescriptionBlock>();
            GroupParticipants = new HashSet<GroupParticipant>();
            GroupPosts = new HashSet<GroupPost>();
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
        public virtual ICollection<GroupParticipant> GroupParticipants { get; set; }
        public virtual ICollection<GroupPost> GroupPosts { get; set; }
    }
}
