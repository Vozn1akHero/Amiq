using Amiq.Contracts.User;
using System;

namespace Amiq.Contracts.Chat
{
    public class DtoChatMessage
    {
        public Guid MessageId { get; set; }
        public Guid ChatId { get; set; }
        public string TextContent { get; set; }
        public DtoShortUserInfo Author { get; set; }
        public DtoShortUserInfo Receiver { get; set; }
        //public int ReceiverId { get; set; }
    }
}
