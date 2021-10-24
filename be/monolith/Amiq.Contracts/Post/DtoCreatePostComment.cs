using System;

namespace Amiq.Contracts.Post
{
    public class DtoCreatePostComment
    {
        public Guid PostId { get; set; }
        public string TextContent { get; set; }
        public string AuthorVisibilityType { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? MainParentId { get; set; }
        public int? GroupId { get; set; }
    }
}
