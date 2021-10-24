using Amiq.Contracts.Group;
using Amiq.Contracts.User;
using System;
using System.Collections.Generic;

namespace Amiq.Contracts.Post
{
    public class DtoPostComment
    {
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public int AuthorId { get; set; }
        public string TextContent { get; set; }
        public DtoUserInfo Author { get; set; }
        public DtoGroup Group { get; set; }
        //public bool IsChild { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? ParentCommentId { get; set; }
        public DtoPostComment Parent { get; set; }
        public List<DtoPostComment> Children { get; set; }
        public string AuthorVisibilityType { get; set; }
    }
}
