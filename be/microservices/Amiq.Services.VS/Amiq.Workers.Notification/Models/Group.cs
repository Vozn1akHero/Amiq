using System;
using System.Collections.Generic;

namespace Amiq.Workers.Notification.Models
{
    public partial class Group
    {
        public Group()
        {
            GroupPosts = new HashSet<GroupPost>();
            GroupVisitations = new HashSet<GroupVisitation>();
        }

        public int GroupId { get; set; }
        public string Name { get; set; } = null!;
        public string AvatarSrc { get; set; } = null!;

        public virtual ICollection<GroupPost> GroupPosts { get; set; }
        public virtual ICollection<GroupVisitation> GroupVisitations { get; set; }
    }
}
