using System;
using System.Collections.Generic;

namespace Amiq.Services.Post.DataAccessLayer.Models.Models
{
    public partial class UserPost
    {
        public Guid UserPostId { get; set; }
        public Guid PostId { get; set; }
        public int UserId { get; set; }

        public virtual Post Post { get; set; } = null!;
    }
}
