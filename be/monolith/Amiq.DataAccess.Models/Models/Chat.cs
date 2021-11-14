using System;
using System.Collections.Generic;

namespace Amiq.DataAccess.Models.Models
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

        public virtual User FirstUser { get; set; }
        public virtual User SecondUser { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
