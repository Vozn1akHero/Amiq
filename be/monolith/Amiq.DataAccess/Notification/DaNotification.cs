using Amiq.Common;
using Amiq.Contracts;
using Amiq.Contracts.Notification;
using Amiq.Contracts.Utils;
using Amiq.Contracts.User;
using Amiq.DataAccess.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccess.Notification
{
    public class DaNotification
    {
        private AmiqContext _amiqContext = new AmiqContext();

        public async Task<DtoListResponseOf<DtoNotification>> GetNotificationsAsync(int userId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            DtoListResponseOf<DtoNotification> result = new();
            IQueryable<DtoNotification> query = _amiqContext.Notifications.Where(e => e.UserId == userId)
                .Select(e => new DtoNotification
                {
                    NotificationId = e.NotificationId,
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
                    NotificationType = e.NotificationTypeId.HasValue ? new DtoNotificationType { 
                        NotificationTypeId = e.NotificationType.NotificationTypeId,
                        NotificationType = e.NotificationType.Name
                    } : null,
                })
                .Paginate(dtoPaginatedRequest.Page, dtoPaginatedRequest.Count);
            result.Entities = await query.ToListAsync();
            result.Length = await query.CountAsync();
            return result;
        }

        public async Task CreateNotificationsAsync(IEnumerable<Models.Models.Notification> notifications)
        {
            await _amiqContext.Notifications.AddRangeAsync(notifications);
            await _amiqContext.SaveChangesAsync();
        }
    }
}
