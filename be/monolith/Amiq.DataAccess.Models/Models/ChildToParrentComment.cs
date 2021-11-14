using System;
using System.Collections.Generic;

namespace Amiq.DataAccess.Models.Models
{
    public partial class ChildToParrentComment
    {
        public Guid ChildToParentCommentId { get; set; }
        public Guid? MainParentId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? CommentId { get; set; }
    }
}
