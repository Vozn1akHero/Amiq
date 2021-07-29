using System;
using System.Collections.Generic;

#nullable disable

namespace temp.Models
{
    public partial class Group
    {
        public Group()
        {
            GroupParticipants = new HashSet<GroupParticipant>();
        }

        public int GroupId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<GroupParticipant> GroupParticipants { get; set; }
    }
}
