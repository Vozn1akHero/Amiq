using System;
using System.Collections.Generic;

#nullable disable

namespace Amiq.DataAccess.Models.Models
{
    public partial class Comment
    {
        public Comment()
        {
            InverseMainParent = new HashSet<Comment>();
            InverseParent = new HashSet<Comment>();
        }

        public Guid CommentId { get; set; }
        public string TextContent { get; set; }
        public Guid PostId { get; set; }
        public int AuthorId { get; set; }
        public Guid? ParentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? EditedBy { get; set; }
        public DateTime? EditedAt { get; set; }
        public Guid? MainParentId { get; set; }
        public int? GroupId { get; set; }

        public virtual User Author { get; set; }
        public virtual Group Group { get; set; }
        public virtual Comment MainParent { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<Comment> InverseMainParent { get; set; }
        public virtual ICollection<Comment> InverseParent { get; set; }
    }
}
