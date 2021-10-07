using System;
using System.Collections.Generic;

namespace Amiq.Contracts.Chat
{
    public class DtoDeleteChatMessagesRequest
    {
        public List<Guid> MessageIds { get; set; }
    }
}
