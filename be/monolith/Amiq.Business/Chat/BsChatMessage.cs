using Amiq.Business.Chat.BsRules;
using Amiq.Business.User.BsRule;
using Amiq.Business.Utils;
using Amiq.Contracts;
using Amiq.Contracts.Chat;
using Amiq.Contracts.Utils;
using Amiq.DataAccess.Chat;
using Amiq.DataAccess.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amiq.Business.Chat
{
    public class BsChatMessage : BsServiceBase
    {
        private DaChatMessage _daChatMessage = new DaChatMessage();
        private DaBlockedUser _daBlockedUser = new DaBlockedUser();

        public async Task<DtoChatMessage> CreateChatMessageAsync(DtoChatMessageCreation dtoChatMessageCreation)
        {
            CheckBsRule(new ChatShouldBeAvailableForInterlocutor(dtoChatMessageCreation.AuthorId, dtoChatMessageCreation.ChatId));
            CheckBsRule(new BsRuleBlockedUserCannotPerformAction(_daBlockedUser, dtoChatMessageCreation.AuthorId, dtoChatMessageCreation.ReceiverId));
            return await _daChatMessage.CreateChatMessageAsync(dtoChatMessageCreation);
        }

        public async Task<IReadOnlyList<DtoChatMessage>> GetChatMessagesAsync(int requestIssuerId,
            Guid chatId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            CheckBsRule(new ChatShouldBeAvailableForInterlocutor(requestIssuerId, chatId));
            return await _daChatMessage.GetChatMessagesAsync(chatId, dtoPaginatedRequest);
        }

        public async Task<List<DtoChatPreview>> GetChatPreviewListAsync(int userId, DtoPaginatedRequest dtoChatPreviewListRequest)
            => await _daChatMessage.GetChatPreviewListAsync(userId, dtoChatPreviewListRequest);

        public async Task<DtoDeleteEntityResponse> DeleteMessageAsync(DtoDeleteChatMessageRequest dtoDeleteChatMessageRequest)
        {
            CheckBsRule(new ChatShouldBeAvailableForInterlocutor(dtoDeleteChatMessageRequest.IssuerId, dtoDeleteChatMessageRequest.ChatId));
            return await _daChatMessage.DeleteMessageAsync(dtoDeleteChatMessageRequest);
        }

        public async Task<DtoDeleteEntitiesResponse> DeleteMessagesAsync(int issuerId, IEnumerable<Guid> messageIds)
        {
            await CheckBsRuleAsync(new UserCanRemoveOwnMessagesOnlyAsync(_daChatMessage, issuerId, messageIds));
            return await _daChatMessage.DeleteMessages(messageIds);
        }
    }
}
