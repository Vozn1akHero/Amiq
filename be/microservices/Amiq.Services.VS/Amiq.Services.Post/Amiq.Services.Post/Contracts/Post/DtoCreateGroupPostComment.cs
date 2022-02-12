using System;

namespace Amiq.Services.Post.Contracts.Post
{
    public class DtoCreateGroupPostComment : DtoCreatePostComment
    {
        public int GroupId { get; set; }
        public string AuthorVisibilityType { get; set; }
        public Guid? GroupCommentParentId { get; set; }
        public Guid? GroupCommentMainParentId { get; set; }
    }
}
