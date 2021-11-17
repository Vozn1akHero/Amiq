using Amiq.Contracts;
using Amiq.Contracts.Chat;
using Amiq.Contracts.User;
using Amiq.Contracts.Utils;
using Amiq.DataAccess.Models.Models;
using Amiq.Mapping;
using AutoMapper.QueryableExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccess.Chat
{
    public class DaChatMessage
    {
        private AmiqContext _amiqContext = new AmiqContext();

        public async Task<DtoChatMessage> CreateChatMessageAsync(DtoChatMessageCreation dtoChatMessage)
        {
            Message chatMessage = APAutoMapper.Instance.Map<Message>(dtoChatMessage);
            _amiqContext.Add(chatMessage);
            await _amiqContext.SaveChangesAsync();
            var query = _amiqContext.Messages.Where(e=>e.MessageId == chatMessage.MessageId);
            //var mappedMsg = APAutoMapper.Instance.Map<DtoChatMessage>(chatMessage);
            var mappedMsg = APAutoMapper.Instance.ProjectTo<DtoChatMessage>(query).First();
            return mappedMsg;
        }

        public async Task<DtoListResponseOf<DtoChatMessage>> GetChatMessagesAsync(Guid chatId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            var result = new DtoListResponseOf<DtoChatMessage>();
            result.Entities = await _amiqContext.Messages.Where(e => e.ChatId == chatId)
                .OrderByDescending(e=>e.CreatedAt)
                .Select(e=> new DtoChatMessage
                {
                    MessageId = e.MessageId,
                    ChatId = e.ChatId,
                    TextContent = e.TextContent,
                    CreatedAt = e.CreatedAt,
                    Author = new DtoBasicUserInfo
                    {
                        UserId = e.AuthorId,
                        Name = e.Author.Name,
                        Surname = e.Author.Name,
                        AvatarPath = e.Author.AvatarPath
                    },
                    Receiver = new DtoBasicUserInfo
                    {
                        UserId = e.AuthorId == e.Chat.FirstUserId ? e.Chat.SecondUserId : e.Chat.FirstUserId,
                        Name = e.AuthorId == e.Chat.FirstUserId ? e.Chat.SecondUser.Name : e.Chat.FirstUser.Name,
                        Surname = e.AuthorId == e.Chat.FirstUserId ? e.Chat.SecondUser.Surname : e.Chat.FirstUser.Surname,
                        AvatarPath = e.AuthorId == e.Chat.FirstUserId ? e.Chat.SecondUser.AvatarPath : e.Chat.FirstUser.AvatarPath
                    }
                })
                .Skip((dtoPaginatedRequest.Page - 1) * dtoPaginatedRequest.Count)
                .Take(dtoPaginatedRequest.Count)
                .ToListAsync();
            //result.Entities = await APAutoMapper.Instance.ProjectTo<DtoChatMessage>(query).ToListAsync();
            result.Length = await _amiqContext.Messages.Where(e => e.ChatId == chatId).CountAsync();
            return result;
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

        /// <summary>
        /// Zwraca listę ostatnich wiadomości w czatach
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dtoChatPreviewListRequest"></param>
        /// <returns></returns>
        public async Task<List<DtoChatPreview>> GetChatPreviewListAsync(int userId, DtoPaginatedRequest dtoChatPreviewListRequest)
        {
            var data =  await _amiqContext.Chats
                        .Where(e => (e.FirstUserId == userId || e.SecondUserId == userId) && e.Messages != null && e.Messages.Count > 0)
                        .Select(g => new { Msg = g.Messages.OrderByDescending(p => p.CreatedAt).First(), Chat = g })
                        .Select(e => new DtoChatPreview
                         {
                             ChatId = e.Msg.ChatId,
                             MessageId = e.Msg.MessageId,
                             Author = new Contracts.User.DtoBasicUserInfo { 
                                 UserId = e.Msg.Author.UserId, 
                                 Name = e.Msg.Author.Name, 
                                 Surname = e.Msg.Author.Surname, 
                                 AvatarPath = e.Msg.Author.AvatarPath },
                            TextContent = e.Msg.TextContent,
                            Interlocutor = new Contracts.User.DtoBasicUserInfo
                            {
                                UserId = e.Chat.FirstUserId == userId ? e.Chat.SecondUser.UserId : e.Chat.FirstUser.UserId,
                                Name = e.Chat.FirstUserId == userId ? e.Chat.SecondUser.Name : e.Chat.FirstUser.Name,
                                Surname = e.Chat.FirstUserId == userId ? e.Chat.SecondUser.Surname : e.Chat.FirstUser.Surname,
                                AvatarPath = e.Chat.FirstUserId == userId ? e.Chat.SecondUser.AvatarPath : e.Chat.FirstUser.AvatarPath
                            },
                            Date = e.Msg.CreatedAt,
                            WrittenByIssuer = e.Msg.AuthorId == userId
                        })
                        .ToListAsync();
            return data;
        }

        public async Task<List<DtoChatPreview>> SearchForChatsAsync(int userId, string text)
        {
            var data = await _amiqContext.Chats
                        .Where(e => (e.FirstUserId == userId || e.SecondUserId == userId)
                            && e.Messages != null
                            && e.Messages.Count > 0
                            && (e.FirstUserId == userId ? (e.SecondUser.Name + e.SecondUser.Surname).StartsWith(text)
                                : (e.FirstUser.Name + e.FirstUser.Surname).StartsWith(text)))
                        .Select(g => new { Msg = g.Messages.OrderByDescending(p => p.CreatedAt).First(), Chat = g })
                        .Select(e => new DtoChatPreview
                        {
                            ChatId = e.Msg.ChatId,
                            MessageId = e.Msg.MessageId,
                            Author = new DtoBasicUserInfo
                            {
                                UserId = e.Msg.Author.UserId,
                                Name = e.Msg.Author.Name,
                                Surname = e.Msg.Author.Surname,
                                AvatarPath = e.Msg.Author.AvatarPath
                            },
                            TextContent = e.Msg.TextContent,
                            Interlocutor = new DtoBasicUserInfo
                            {
                                UserId = e.Chat.FirstUserId == userId ? e.Chat.SecondUser.UserId : e.Chat.FirstUser.UserId,
                                Name = e.Chat.FirstUserId == userId ? e.Chat.SecondUser.Name : e.Chat.FirstUser.Name,
                                Surname = e.Chat.FirstUserId == userId ? e.Chat.SecondUser.Surname : e.Chat.FirstUser.Surname,
                                AvatarPath = e.Chat.FirstUserId == userId ? e.Chat.SecondUser.AvatarPath : e.Chat.FirstUser.AvatarPath
                            },
                            Date = e.Msg.CreatedAt,
                            WrittenByIssuer = e.Msg.AuthorId == userId
                        })
                        .ToListAsync();
            return data;
        }

        public async Task<DtoDeleteEntityResponse> DeleteMessageAsync(DtoDeleteChatMessageRequest dtoDeleteChatMessageRequest)
        {
            DtoDeleteEntityResponse dtoDeleteEntityResponse = new();
            var message = _amiqContext.Messages.SingleOrDefault(e=>e.ChatId==dtoDeleteChatMessageRequest.ChatId
                && e.MessageId == dtoDeleteChatMessageRequest.MessageId);
            if(message != null)
            {
                dtoDeleteEntityResponse.IsBusinessException = false;
                dtoDeleteEntityResponse.Entity = DaResults.EntityIsNotFound;
                _amiqContext.Remove(message);
                await _amiqContext.SaveChangesAsync();
            }
            else
            {
                dtoDeleteEntityResponse.IsBusinessException = true;
            }
            return dtoDeleteEntityResponse;
        }

        public async Task<DtoDeleteEntitiesResponse> DeleteMessages(IEnumerable<Guid> messageIds)
        {
            var result = new DtoDeleteEntitiesResponse();
            var entities = _amiqContext.Messages.Where(e => messageIds.Contains(e.MessageId)).ToList();
            _amiqContext.Messages.RemoveRange(entities);
            await _amiqContext.SaveChangesAsync();
            result.Entities = APAutoMapper.Instance.Map<List<DtoChatMessage>>(entities);
            return result;
        }

        public async Task<bool> AreMessagesCreatedByUserAsync(int userId, IEnumerable<Guid> messageIds)
        {
            var entities = await _amiqContext.Messages.Where(e => messageIds.Contains(e.MessageId)).ToListAsync();
            bool result = !entities.Any(e => e.AuthorId != userId);
            return result;
        }
    }

    /*public static class SqlQueryExtensions
    {
        public static IList<T> SqlQuery<T>(this DbContext db, string sql, params object[] parameters) where T : class
        {
            using (var db2 = new ContextForQueryType<T>(db.Database.GetDbConnection()))
            {
                var res = db2.Set<T>().FromSqlRaw(sql, parameters);
                return res.ToList();
            }
        }

        private class ContextForQueryType<T> : DbContext where T : class
        {
            private readonly DbConnection connection;

            public ContextForQueryType(DbConnection connection)
            {
                this.connection = connection;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(connection, options => options.EnableRetryOnFailure());

                base.OnConfiguring(optionsBuilder);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<T>().HasNoKey();
                base.OnModelCreating(modelBuilder);
            }
        }
    }*/

}


