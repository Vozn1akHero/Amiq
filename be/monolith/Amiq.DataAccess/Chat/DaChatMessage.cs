using Amiq.Contracts;
using Amiq.Contracts.Chat;
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
            var mappedMsg = APAutoMapper.Instance.Map<DtoChatMessage>(chatMessage);
            return mappedMsg;
        }

        public async Task<List<DtoChatMessage>> GetChatMessagesAsync(Guid chatId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            var query = _amiqContext.Messages.Where(e => e.ChatId == chatId)
                .Skip((dtoPaginatedRequest.Page - 1) * dtoPaginatedRequest.Count)
                .Take(dtoPaginatedRequest.Count);
            var data = await APAutoMapper.Instance.ProjectTo<DtoChatMessage>(query).ToListAsync();
            return data;
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

        public async Task<List<DtoChatPreview>> GetChatPreviewListAsync(int userId, DtoPaginatedRequest dtoChatPreviewListRequest)
        {
            var param = new SqlParameter[] {
                    new SqlParameter() {
                        ParameterName = "@userId",
                        SqlDbType =  System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = userId
                    },
                    new SqlParameter() {
                        ParameterName = "@length",
                        SqlDbType =  System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = dtoChatPreviewListRequest.Count
                    },
                    new SqlParameter() {
                        ParameterName = "@skip",
                        SqlDbType =  System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = (dtoChatPreviewListRequest.Page - 1) * dtoChatPreviewListRequest.Count
                    }
            };

            /*var data =  _amiqContext.Messages
                .FromSqlRaw("Chat.GetChatPreviews @userId, @length, @skip", param)
                .ToList();*/
            var data = _amiqContext.SqlQuery<DtoChatPreview>("EXECUTE Chat.GetChatPreviews @userId, @length, @skip", param);
            var dataC = data.Select(e => new DtoChatPreview
            {
                
            }).ToList();
            //var data = _amiqContext.Database.ExecuteSqlRaw

            return dataC/*.Select(e => new DtoChatPreview
            {
                ChatId = e.ChatId,
                MessageAuthorId = e.AuthorId,
                AuthorAvatarPath = e.Author.AvatarPath,
                AuthorName = e.Author.Name,
                AuthorSurname = e.Author.Surname,
                Message = e.TextContent
            }).ToList()*/;
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

    public static class SqlQueryExtensions
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
    }

}


