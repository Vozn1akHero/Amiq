using Amiq.Contracts.Chat;
using Amiq.DataAccessLayer.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccessLayer.Chat
{
    public class DaoChat
    {
        private AmiqContext _amiqContext = new AmiqContext();

        public bool IsUserChatParticipant(int userId, Guid chatId)
        {
            bool res = _amiqContext.Chats.Any(e => (e.FirstUserId == userId || e.SecondUserId == userId) && e.ChatId == chatId);
            return res;
        }

        
    }
}
