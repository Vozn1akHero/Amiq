using System;
using System.Collections.Generic;

#nullable disable

namespace AmicaPlus.DataAccess.Models.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            GroupPosts = new HashSet<GroupPost>();
        }

        public Guid PostId { get; set; }
        public string TextContent { get; set; }

        public virtual UserPost UserPost { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<GroupPost> GroupPosts { get; set; }
    }
}
