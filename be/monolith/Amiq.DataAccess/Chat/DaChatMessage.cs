using Amiq.Contracts;
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

        public async Task<List<DtoChatPreview>> GetChatPreviewListAsync(DtoChatPreviewListRequest dtoChatPreviewListRequest)
        {
            return await _amiqContext.Messages
                .FromSqlRaw("Chat.GetChatPreviews @userId, @length, @skip", dtoChatPreviewListRequest.IssuerId, 
                    dtoChatPreviewListRequest.Count,
                    ((dtoChatPreviewListRequest.Page-1) * dtoChatPreviewListRequest.Count))
                .Select(e => new DtoChatPreview
                {
                    ChatId = e.ChatId,
                    MessageAuthorId = e.AuthorId,
                    AuthorAvatarPath = e.Author.AvatarPath,
                    AuthorName = e.Author.Name,
                    AuthorSurname = e.Author.Surname,
                    Message = e.TextContent
                })
                .ToListAsync();
        }

        public async Task<DtoDeleteEntityResponse> DeleteMessageAsync(DtoDeleteChatMessageRequest dtoDeleteChatMessageRequest)
        {
            DtoDeleteEntityResponse dtoDeleteEntityResponse = new();
            var message = _amiqContext.Messages.SingleOrDefault(e=>e.ChatId==dtoDeleteChatMessageRequest.ChatId
                && e.MessageId == dtoDeleteChatMessageRequest.MessageId);
            if(message != null)
            {
                dtoDeleteEntityResponse.Result = true;
                dtoDeleteEntityResponse.Entity = DaResults.EntityIsNotFound;
                _amiqContext.Remove(message);
                await _amiqContext.SaveChangesAsync();
            }
            else
            {
                dtoDeleteEntityResponse.Result = false;
            }
            return dtoDeleteEntityResponse;
        }
    }
}
