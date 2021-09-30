using Amiq.Contracts.Utils;

namespace Amiq.Contracts.Chat
{
    public class DtoChatPreviewListRequest : DtoPaginatedRequest
    {
        public int IssuerId { get; set; }
    }
}
