using System;
using System.Collections.Generic;

#nullable disable

namespace AmicaPlus.DataAccess.Models
{
    public partial class Message
    {
        public Guid MessageId { get; set; }
        public Guid ChatId { get; set; }
        public string TextContent { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Chat Chat { get; set; }
    }
}
