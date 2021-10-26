using Amiq.Contracts.Group;
using Amiq.Contracts.User;
using System;
using System.Collections.Generic;

namespace Amiq.Contracts.Post
{
    public class DtoGroupPostComment //: DtoPostComment
    {
        public DtoGroup Group { get; set; }
        public string AuthorVisibilityType { get; set; }
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public string TextContent { get; set; }
        public DtoBasicUserInfo Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? ParentCommentId { get; set; }
        public Guid? MainParentCommentId { get; set; }
        public List<DtoGroupPostComment> Children { get; set; }
    }
}
