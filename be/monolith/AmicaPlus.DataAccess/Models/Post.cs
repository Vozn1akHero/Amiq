using System;
using System.Collections.Generic;

#nullable disable

namespace AmicaPlus.DataAccess.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        public Guid PostId { get; set; }
        public string TextContent { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
