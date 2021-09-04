using Amiq.Business.Utils;
using Amiq.Contracts.Chat;
using Amiq.Contracts.Utils;
using Amiq.DataAccess.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business.Chat
{
    public class BsChatMessage : BsServiceBase
    {
        private DaChatMessage _daChatMessage = new DaChatMessage();

        public async Task CreateChatMessageAsync(DtoChatMessage dtoChatMessage)
        {
            await _daChatMessage.CreateChatMessageAsync(dtoChatMessage);
        }

        public async Task<IReadOnlyList<DtoChatMessage>> GetChatMessagesAsync(DtoPaginatedRequest dtoPaginatedRequest)
        {
            return await _daChatMessage.GetChatMessagesAsync(dtoPaginatedRequest);
        }

        public async Task<List<DtoChatPreview>> GetChatPreviewListAsync(DtoChatPreviewListRequest dtoChatPreviewListRequest)
        => await _daChatMessage.GetChatPreviewListAsync(dtoChatPreviewListRequest);
    }
}
