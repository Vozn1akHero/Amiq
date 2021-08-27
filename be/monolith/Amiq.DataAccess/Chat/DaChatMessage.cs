using Amiq.Contracts.Chat;
using Amiq.Contracts.Utils;
using Amiq.DataAccess.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccess.Chat
{
    public class DaChatMessage
    {
        private AmiqContext _amiqContext = new AmiqContext();

        public async Task CreateChatMessageAsync(DtoChatMessage dtoChatMessage)
        {
            var chatMessage = new Message { };
            _amiqContext.Add(chatMessage);
            await _amiqContext.SaveChangesAsync();
        }

        public async Task<List<DtoChatMessage>> GetChatMessagesAsync(DtoPaginatedRequest dtoPaginatedRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DtoChatPreview>> GetChatMessagesAsync(DtoChatPreviewListRequest dtoPaginatedRequest)
        {
            var previews = new List<DtoChatPreview>();
            previews = await (from c in _amiqContext.Chats.AsNoTracking()
                              join m in _amiqContext.Messages.AsNoTracking()
                              on c.ChatId equals m.ChatId
                              select new DtoChatPreview {
                                
                              }).ToListAsync();
            return previews;
        }
    }
}
