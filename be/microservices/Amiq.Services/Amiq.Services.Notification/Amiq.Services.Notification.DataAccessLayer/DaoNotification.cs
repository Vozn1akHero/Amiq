using Amiq.Services.Notification.Common;
using Amiq.Services.Notification.Common.DbOperation;
using Amiq.Services.Notification.Contracts.Notification;
using Amiq.Services.Notification.Contracts.User;
using Amiq.Services.Notification.Contracts.Utils;
using Amiq.Services.Notification.DataAccessLayer.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Amiq.Services.Notification.DataAccessLayer
{
    public class DaoNotification
    {
        private AmiqNotificationContext _amiqContext = new AmiqNotificationContext();

        public async Task<DtoListResponseOf<DtoNotification>> GetNotificationsAsync(int userId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            DtoListResponseOf<DtoNotification> result = new();
            IQueryable<DtoNotification> query = _amiqContext.Notifications.Where(e => e.UserId == userId)
                .OrderByDescending(e => e.CreatedAt)
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
            foreach (var entry in entries)
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
