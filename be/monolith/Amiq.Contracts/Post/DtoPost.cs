using System;

namespace Amiq.Contracts.Post
{
    public class DtoPost
    {
        public Guid PostId { get; set; }
        public string TextContent { get; set; }
        public int UserId { get; set; }
        public int? EditedBy { get; set; }
        public DateTime? EditedAt {  get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
