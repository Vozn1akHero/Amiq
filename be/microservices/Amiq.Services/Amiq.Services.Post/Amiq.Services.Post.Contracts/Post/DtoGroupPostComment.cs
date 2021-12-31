using Amiq.Contracts.Group;
using Amiq.Contracts.User;
using System;
using System.Collections.Generic;

namespace Amiq.Services.Post.Contracts.Post
{
    public class DtoGroupPostComment
    {
        public DtoMinifiedGroup Group { get; set; }
        public Guid GroupPostCommentId { get; set; }
        public string AuthorVisibilityType { get; set; }
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public string TextContent { get; set; }
        public DtoBasicUserInfo Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? ParentCommentId { get; set; }
        public DtoBasicUserInfo ParentCommentAuthor { get; set; }
        public Guid? MainParentCommentId { get; set; }
        public List<DtoGroupPostComment> Children { get; set; }
        public bool IsRemoved { get; set; }
        public Guid? GroupCommentParentId { get; set; }
        public Guid? GroupCommentMainParentId { get; set; }
    }
}
