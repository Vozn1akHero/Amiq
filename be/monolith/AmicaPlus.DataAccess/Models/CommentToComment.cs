using System;
using System.Collections.Generic;

#nullable disable

namespace temp.Models
{
    public partial class CommentToComment
    {
        public Guid CommentToCommentId { get; set; }
        public Guid ParentCommentId { get; set; }
        public Guid ChildCommentId { get; set; }

        public virtual Comment ChildComment { get; set; }
        public virtual Comment ParentComment { get; set; }
    }
}
