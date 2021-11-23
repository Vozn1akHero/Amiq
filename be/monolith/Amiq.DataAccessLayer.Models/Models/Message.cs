using System;
using System.Collections.Generic;

namespace Amiq.DataAccess.Models.Models
{
    public partial class Message
    {
        public Guid MessageId { get; set; }
        public Guid ChatId { get; set; }
        public string TextContent { get; set; }
        public DateTime CreatedAt { get; set; }
        public int AuthorId { get; set; }
        public bool IsReadByReceiver { get; set; }

        public virtual User Author { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
