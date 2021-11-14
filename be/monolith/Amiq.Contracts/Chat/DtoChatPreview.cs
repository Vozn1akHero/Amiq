using Amiq.Contracts.User;
using System;

namespace Amiq.Contracts.Chat
{
    public class DtoChatPreview
    {
        public Guid MessageId { get; set; }
        public Guid ChatId { get; set; }
        public string TextContent { get; set; }
        public DtoBasicUserInfo Author { get; set; }
        public DtoBasicUserInfo Interlocutor { get; set; }
        public bool WrittenByIssuer { get; set; }
        public DateTime Date { get; set; }

    }
}
