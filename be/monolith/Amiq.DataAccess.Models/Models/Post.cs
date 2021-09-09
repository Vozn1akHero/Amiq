using System;
using System.Collections.Generic;

#nullable disable

namespace Amiq.DataAccess.Models.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            GroupPosts = new HashSet<GroupPost>();
            UserPosts = new HashSet<UserPost>();
        }

        public Guid PostId { get; set; }
        public string TextContent { get; set; }
        public int? EditedBy { get; set; }
        public DateTime? EditedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual User EditedByNavigation { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<GroupPost> GroupPosts { get; set; }
        public virtual ICollection<UserPost> UserPosts { get; set; }
    }
}
