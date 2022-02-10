using Amiq.BusinessLayer.Utils;
using Amiq.DataAccessLayer.Chat;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amiq.BusinessLayer.Chat.BsRules
{
    public class UserCanRemoveOwnMessagesOnlyAsync : IBsRuleAsync
    {
        public string ErrorContent => "Użytkownik może usuwać tylko własne wiadomości";

        private DaoChatMessage _daChatMessage;
        private int _issuerId;
        private IEnumerable<Guid> _messageIds;

        public UserCanRemoveOwnMessagesOnlyAsync(DaoChatMessage daChatMessage, int issuerId, IEnumerable<Guid> messageIds)
        {
            _daChatMessage = daChatMessage ?? throw new ArgumentException(nameof(daChatMessage));
            _issuerId = issuerId;
            _messageIds = messageIds ?? throw new ArgumentException(nameof(messageIds));
        }

        public async Task<bool> CheckBsRuleAsync()
        {
            return await _daChatMessage.AreMessagesCreatedByUserAsync(_issuerId, _messageIds);
        }
    }
}
