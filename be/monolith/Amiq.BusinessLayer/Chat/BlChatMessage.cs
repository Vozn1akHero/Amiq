using Amiq.BusinessLayer.Chat.BsRules;
using Amiq.BusinessLayer.User.BsRule;
using Amiq.BusinessLayer.Utils;
using Amiq.Contracts.Chat;
using Amiq.Contracts.Utils;
using Amiq.DataAccessLayer.Chat;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amiq.BusinessLayer.Chat
{
    public class BlChatMessage : BusinessLayerBase
    {
        private DaoChatMessage _daChatMessage = new DaoChatMessage();

        public async Task<DtoChatMessage> CreateChatMessageAsync(DtoChatMessageCreation dtoChatMessageCreation)
        {
            CheckBsRule(new ChatShouldBeAvailableForInterlocutor(dtoChatMessageCreation.AuthorId, dtoChatMessageCreation.ChatId));
            CheckBsRule(new BsRuleCannotPerformActionOnCommonBlock(dtoChatMessageCreation.AuthorId, dtoChatMessageCreation.ReceiverId));
            return await _daChatMessage.CreateChatMessageAsync(dtoChatMessageCreation);
        }

        public async Task<DtoListResponseOf<DtoChatMessage>> GetChatMessagesAsync(int requestIssuerId,
            Guid chatId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            CheckBsRule(new ChatShouldBeAvailableForInterlocutor(requestIssuerId, chatId));
            return await _daChatMessage.GetChatMessagesAsync(chatId, dtoPaginatedRequest);
        }

        public async Task<List<DtoChatPreview>> GetChatPreviewListAsync(int userId, DtoPaginatedRequest dtoChatPreviewListRequest)
            => await _daChatMessage.GetChatPreviewListAsync(userId, dtoChatPreviewListRequest);

        public async Task<List<DtoChatPreview>> SearchForChatsAsync(int userId, string text) =>
            await _daChatMessage.SearchForChatsAsync(userId, text);

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
