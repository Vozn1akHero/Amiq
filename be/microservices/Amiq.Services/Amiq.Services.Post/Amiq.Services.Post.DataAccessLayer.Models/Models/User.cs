using System;
using System.Collections.Generic;

namespace Amiq.Services.Post.DataAccessLayer.Models.Models
{
    public partial class User
    {
        public User()
        {
            Posts = new HashSet<Post>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string AvatarPath { get; set; } = null!;

        public virtual ICollection<Post> Posts { get; set; }
    }
}
