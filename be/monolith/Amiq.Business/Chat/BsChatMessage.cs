using Amiq.Business.User.BsRule;
using Amiq.Business.Utils;
using Amiq.Contracts;
using Amiq.Contracts.Chat;
using Amiq.Contracts.Utils;
using Amiq.DataAccess.Chat;
using Amiq.DataAccess.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amiq.Business.Chat
{
    public class BsChatMessage : BsServiceBase
    {
        private DaChatMessage _daChatMessage = new DaChatMessage();
        private DaBlockedUser _daBlockedUser = new DaBlockedUser();

        public async Task CreateChatMessageAsync(DtoChatMessage dtoChatMessage)
        {
            CheckBsRule(new BsRuleBlockedUserCannotPerformAction(_daBlockedUser, dtoChatMessage.AuthorId, dtoChatMessage.ReceiverId));
            await _daChatMessage.CreateChatMessageAsync(dtoChatMessage);
        }

        public async Task<IReadOnlyList<DtoChatMessage>> GetChatMessagesAsync(DtoPaginatedRequest dtoPaginatedRequest)
        {
            return await _daChatMessage.GetChatMessagesAsync(dtoPaginatedRequest);
        }

        public async Task<List<DtoChatPreview>> GetChatPreviewListAsync(DtoChatPreviewListRequest dtoChatPreviewListRequest)
        => await _daChatMessage.GetChatPreviewListAsync(dtoChatPreviewListRequest);

        public async Task<DtoDeleteEntityResponse> DeleteMessageAsync(DtoDeleteChatMessageRequest dtoDeleteChatMessageRequest)
        {
            return await _daChatMessage.DeleteMessageAsync(dtoDeleteChatMessageRequest);
        }
    }
}
