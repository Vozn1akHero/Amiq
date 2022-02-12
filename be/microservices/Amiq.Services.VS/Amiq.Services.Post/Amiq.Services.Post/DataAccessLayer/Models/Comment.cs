using System;
using System.Collections.Generic;

namespace Amiq.Services.Post.DataAccessLayer.Models
{
    /// <summary>
    /// Id grupy, jeśli komentarz został stworzony przez administratora grupy
    /// </summary>
    public partial class Comment
    {
        public Comment()
        {
            GroupPostComments = new HashSet<GroupPostComment>();
            InverseMainParent = new HashSet<Comment>();
            InverseParent = new HashSet<Comment>();
        }

        public Guid CommentId { get; set; }
        public string TextContent { get; set; } = null!;
        public Guid PostId { get; set; }
        public int AuthorId { get; set; }
        public Guid? ParentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? EditedBy { get; set; }
        public DateTime? EditedAt { get; set; }
        public Guid? MainParentId { get; set; }
        public bool IsRemoved { get; set; }

        public virtual User Author { get; set; } = null!;
        public virtual Comment? MainParent { get; set; }
        public virtual Comment? Parent { get; set; }
        public virtual Post Post { get; set; } = null!;
        public virtual ICollection<GroupPostComment> GroupPostComments { get; set; }
        public virtual ICollection<Comment> InverseMainParent { get; set; }
        public virtual ICollection<Comment> InverseParent { get; set; }
    }
}
