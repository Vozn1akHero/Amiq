using System;
using System.Collections.Generic;

#nullable disable

namespace Amiq.DataAccess.Models.Models
{
    public partial class GroupPostComment
    {
        public Guid GroupPostCommentId { get; set; }
        public int GroupId { get; set; }
        public Guid CommentId { get; set; }
        public string AuthorVisibilityType { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual Group Group { get; set; }
    }
}
