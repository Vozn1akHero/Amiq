using Amiq.Contracts.Group;
using Amiq.Contracts.User;
using System;
using System.Collections.Generic;

namespace Amiq.Services.Post.Contracts.Post
{
    public class DtoPostComment
    {
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        //public int? GroupId { get; set; }
        public string TextContent { get; set; }
        //public int AuthorId { get; set; }
        //public string AuthorName { get; set; }
        //public string AuthorSurname { get; set; }
        public DtoBasicUserInfo Author { get; set; }
        //public DtoGroup Group { get; set; }
        //public bool IsChild { get; set; }
        public DateTime CreatedAt { get; set; }
        public DtoBasicUserInfo ParentCommentAuthor { get; set; }
        public Guid? ParentCommentId { get; set; }
        public Guid? MainParentCommentId { get; set; }
        //public DtoPostComment Parent { get; set; }
        //public DtoPostComment MainParent { get; set; }
        public List<DtoPostComment> Children { get; set; }
        public bool IsRemoved { get; set; }
    }
}
