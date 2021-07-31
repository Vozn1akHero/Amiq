using System;
using System.Collections.Generic;

#nullable disable

namespace AmicaPlus.DataAccess.Models.Models
{
    public partial class Comment
    {
        public Comment()
        {
            CommentToComments = new HashSet<CommentToComment>();
        }

        public Guid CommentId { get; set; }
        public string TextContent { get; set; }
        public Guid PostId { get; set; }
        public int AuthorId { get; set; }

        public virtual User Author { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<CommentToComment> CommentToComments { get; set; }
    }
}
