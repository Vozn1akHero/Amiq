using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Contracts.Chat
{
    public class DtoDeleteChatMessageRequest
    {
        public int IssuerId { get; set; }
        public Guid MessageId { get; set; }
        public Guid ChatId { get; set; }
    }
}
