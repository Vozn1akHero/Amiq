using System;
using System.Collections.Generic;

#nullable disable

namespace Amiq.DataAccess.Models.Models
{
    public partial class CommentToComment
    {
        public CommentToComment()
        {
            InverseParentComment = new HashSet<CommentToComment>();
        }

        public Guid CommentToCommentId { get; set; }
        public Guid ParentCommentId { get; set; }
        public Guid ChildCommentId { get; set; }

        public virtual Comment ChildComment { get; set; }
        public virtual CommentToComment ParentComment { get; set; }
        public virtual ICollection<CommentToComment> InverseParentComment { get; set; }
    }
}
