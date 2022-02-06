using Amiq.Common.Enums;
using Amiq.DataAccessLayer.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Workers.Notification
{
    public class GroupPostNotificationCreation : NotificationCreationStrategy
    {
        public override IEnumerable<DataAccessLayer.Models.Models.Notification> Create(IEnumerable<UserNotificationsQueue> users)
        {
            List<DataAccessLayer.Models.Models.Notification> notifications = new();

            foreach(var user in users)
            {
                int userId = user.UserId;
                Guid notificationGroupId = user.NotificationGroupId;

                var mostVisitedGroups = DbContext.GroupVisitations.AsNoTracking()
                        .Where(e => e.UserId == userId)
                        .Include(e=>e.Group)
                        .Take(5)
                        .OrderByDescending(e => e.VisitationTotalTime)
                        .ToList();
                foreach (var mostVisitedGroup in mostVisitedGroups)
                {
                    bool isParticipant = DbContext.GroupParticipants.AsNoTracking().Any(e=>e.UserId == userId && e.GroupId == mostVisitedGroup.GroupId);
                    if (!isParticipant) continue;

                    var groupPosts = DbContext.GroupPosts
                        .AsNoTracking()
                        .Where(e => e.Post.CreatedAt > mostVisitedGroup.LastVisited
                            && e.GroupId == mostVisitedGroup.GroupId)
                        .Include(e=>e.Post)
                        .ToList();

                    if(groupPosts.Count > 0 && groupPosts.Count <= 2)
                    {
                        foreach(var post in groupPosts)
                            notifications.Add(new DataAccessLayer.Models.Models.Notification {
                                ImageSrc = mostVisitedGroup.Group.AvatarSrc,
                                NotificationGroupId = notificationGroupId,
                                Text = $"W grupie {mostVisitedGroup.Group.Name} pojawił się wpis: {new string(post.Post.TextContent.Take(30).ToArray())}...",
                                NotificationType = EnNotificationType.GP.ToString(),
                                Link = $"/group/{mostVisitedGroup.GroupId}",
                                CreatedAt = DateTime.Now,
                                UserId = userId
                            });
                    }
                    else if(groupPosts.Count > 2)
                    {
                        notifications.Add(new DataAccessLayer.Models.Models.Notification
                        {
                            ImageSrc = mostVisitedGroup.Group.AvatarSrc,
                            NotificationGroupId = notificationGroupId,
                            Text = $"W grupie {mostVisitedGroup.Group.Name} pojawiło się {groupPosts.Count} wpisów",
                            Link = $"/group/{mostVisitedGroup.GroupId}",
                            NotificationType = EnNotificationType.GP.ToString(),
                            CreatedAt = DateTime.Now,
                            UserId = userId
                        });;
                    }
                }
            }

            return notifications;
        }
    }
}
