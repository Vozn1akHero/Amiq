using Amiq.Business.Utils;
using Amiq.DataAccessLayer.Chat;
using System;

namespace Amiq.Business.Chat.BsRules
{
    public class ChatShouldBeAvailableForInterlocutor : IBsRule
    {
        private DaoChat _daChat = new DaoChat();
        private int _issuerId;
        private Guid _chatId;

        public string ErrorContent => "Chat data is unavailable for request issuer";

        public ChatShouldBeAvailableForInterlocutor(int issuerId, Guid chatId)
        {
            _issuerId = issuerId;
            _chatId = chatId;
        }

        public bool CheckBsRule()
        {
            return _daChat.IsUserChatParticipant(_issuerId, _chatId);
        }
    }
}
