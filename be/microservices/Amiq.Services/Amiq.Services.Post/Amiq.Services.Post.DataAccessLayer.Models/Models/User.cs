using System;
using System.Collections.Generic;

namespace Amiq.Services.Post.DataAccessLayer.Models.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            GroupPosts = new HashSet<GroupPost>();
            Posts = new HashSet<Post>();
            UserPosts = new HashSet<UserPost>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string AvatarPath { get; set; } = null!;

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<GroupPost> GroupPosts { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<UserPost> UserPosts { get; set; }
    }
}
