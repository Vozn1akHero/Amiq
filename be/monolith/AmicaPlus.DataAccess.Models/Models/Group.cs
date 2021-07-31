using System;
using System.Collections.Generic;

#nullable disable

namespace AmicaPlus.DataAccess.Models.Models
{
    public partial class Group
    {
        public Group()
        {
            GroupDescriptionBlocks = new HashSet<GroupDescriptionBlock>();
            GroupParticipants = new HashSet<GroupParticipant>();
            GroupPosts = new HashSet<GroupPost>();
        }

        public int GroupId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<GroupDescriptionBlock> GroupDescriptionBlocks { get; set; }
        public virtual ICollection<GroupParticipant> GroupParticipants { get; set; }
        public virtual ICollection<GroupPost> GroupPosts { get; set; }
    }
}
