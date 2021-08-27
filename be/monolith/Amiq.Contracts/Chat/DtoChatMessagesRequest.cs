using Amiq.Contracts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Contracts.Chat
{
    public class DtoChatMessagesRequest : DtoPaginatedRequest
    {
        public int RequestIssuerId { get; set; }
        public int ChatId { get; set; }
    }
}
