using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Contracts.Chat
{
    public class DtoChatPreview
    {
        public Guid ChatId { get; set; }
        public int MessageAuthorId { get; set; }
        public string AuthorAvatarPath { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string Message { get; set; }
        public bool HasMedia { get; set; }
    }
}
