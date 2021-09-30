using System;

namespace Amiq.Contracts.Post
{
    public class DtoNewPostComment
    {
        public Guid PostId { get; set; }
        public int AuthorId { get; set; }
        public string Text { get; set; }
    }
}
