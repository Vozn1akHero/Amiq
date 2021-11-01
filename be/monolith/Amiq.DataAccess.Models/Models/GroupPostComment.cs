using System;
using System.Collections.Generic;

#nullable disable

namespace Amiq.DataAccess.Models.Models
{
    public partial class GroupPostComment
    {
        public GroupPostComment()
        {
            InverseMainParent = new HashSet<GroupPostComment>();
            InverseParent = new HashSet<GroupPostComment>();
        }

        public Guid GroupPostCommentId { get; set; }
        public int GroupId { get; set; }
        public Guid CommentId { get; set; }
        public string AuthorVisibilityType { get; set; }
        public Guid? MainParentId { get; set; }
        public Guid? ParentId { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual Group Group { get; set; }
        public virtual GroupPostComment MainParent { get; set; }
        public virtual GroupPostComment Parent { get; set; }
        public virtual ICollection<GroupPostComment> InverseMainParent { get; set; }
        public virtual ICollection<GroupPostComment> InverseParent { get; set; }
    }
}
