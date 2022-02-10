using Amiq.Common;
using Amiq.Contracts.Notification;
using Amiq.Contracts.Utils;
using Amiq.Contracts.User;
using Amiq.DataAccessLayer.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amiq.Common.DbOperation;

namespace Amiq.DataAccessLayer.Notification
{
    public class DaoNotification
    {
        private AmiqContext _amiqContext = new AmiqContext();

        public async Task<DtoListResponseOf<DtoNotification>> GetNotificationsAsync(int userId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            DtoListResponseOf<DtoNotification> result = new();
            IQueryable<DtoNotification> query = _amiqContext.Notifications.Where(e => e.UserId == userId)
                .OrderByDescending(e=>e.CreatedAt)
                .Select(e => new DtoNotification
                {
                    NotificationId = e.NotificationId,
                    NotificationGroupId = e.NotificationGroupId,
                    User = new DtoBasicUserInfo
                    {
                        UserId = e.UserId,
                        Name = e.User.Name,
                        Surname = e.User.Surname,
                        AvatarPath = e.User.AvatarPath
                    },
                    Text = e.Text,
                    ImageSrc = e.ImageSrc,
                    Link = e.Link,
                    CreatedAt = e.CreatedAt,
                    NotificationType = e.NotificationType,
                    IsRead = e.IsRead
                });
            result.Entities = await query.Paginate(dtoPaginatedRequest.Page, dtoPaginatedRequest.Count).ToListAsync();
            result.Length = await query.CountAsync();
            return result;
        }

        public async Task<DbOperationResult> SetAllReadAsync(int userId)
        {
            DbOperationResult dbOperationResult = new();

            var entries = _amiqContext.Notifications.Where(e => e.UserId == userId);
            foreach(var entry in entries)
            {
                entry.IsRead = true;
            }
            await _amiqContext.SaveChangesAsync();
            dbOperationResult.Success = true;

            return dbOperationResult;
        }

        public async Task<DtoNotReadNotificationsExistResult> AnyNotReadExistAsync(int userId)
        {
            DtoNotReadNotificationsExistResult result = new();

            result.Count = await _amiqContext.Notifications.CountAsync(e => e.UserId == userId && !e.IsRead);
            result.Result = result.Count > 0;

            return result;
        }

        public async Task CreateNotificationsAsync(IEnumerable<Models.Models.Notification> notifications)
        {
            await _amiqContext.Notifications.AddRangeAsync(notifications);
            await _amiqContext.SaveChangesAsync();
        }
    }
}
