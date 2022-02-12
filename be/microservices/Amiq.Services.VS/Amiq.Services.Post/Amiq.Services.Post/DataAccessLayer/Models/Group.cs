using System;
using System.Collections.Generic;

namespace Amiq.Services.Post.DataAccessLayer.Models
{
    public partial class Group
    {
        public Group()
        {
            GroupPostComments = new HashSet<GroupPostComment>();
            GroupPosts = new HashSet<GroupPost>();
        }

        public int GroupId { get; set; }
        public string Name { get; set; } = null!;
        public string AvatarSrc { get; set; } = null!;

        public virtual ICollection<GroupPostComment> GroupPostComments { get; set; }
        public virtual ICollection<GroupPost> GroupPosts { get; set; }
    }
}
