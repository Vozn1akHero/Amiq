using System;
using System.Collections.Generic;

namespace Amiq.DataAccessLayer.Models.Models
{
    public partial class Chat
    {
        public Chat()
        {
            Messages = new HashSet<Message>();
        }

        public Guid ChatId { get; set; }
        public int FirstUserId { get; set; }
        public int SecondUserId { get; set; }

        public virtual User FirstUser { get; set; } = null!;
        public virtual User SecondUser { get; set; } = null!;
        public virtual ICollection<Message> Messages { get; set; }
    }
}
