using System;
using System.Collections.Generic;

namespace Amiq.Contracts.Post
{
    public class DtoPostComment
    {
        public Guid PostId { get; set; }
        public int AuthorId { get; set; }
        public string Text { get; set; }
        public List<DtoPostComment> Children { get; set; }
    }
}
