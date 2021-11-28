using Amiq.Business.Utils;
using Amiq.Contracts.Chat;
using Amiq.DataAccessLayer.Chat;
using Amiq.DataAccessLayer.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business.Chat
{
    public class BlChat : BusinessLayerBase
    {
        private DaoChat _daChat = new DaoChat();

        public bool IsUserChatParticipant(int userId, Guid chatId) => _daChat.IsUserChatParticipant(userId, chatId);

    }
}
