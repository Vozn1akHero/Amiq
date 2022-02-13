using Amiq.Services.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace Amiq.Workers.Notification
{
    public class GroupPostNotificationCreation : NotificationCreationStrategy
    {
        public override IEnumerable<Models.Notification> Create(IEnumerable<UserNotificationsQueue> users)
        {
            List<Models.Notification> notifications = new();

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
                        .Where(e => e.CreatedAt > mostVisitedGroup.LastVisited
                            && e.GroupId == mostVisitedGroup.GroupId)
                        .ToList();

                    if(groupPosts.Count > 0 && groupPosts.Count <= 2)
                    {
                        foreach(var post in groupPosts)
                            notifications.Add(new Models.Notification
                            {
                                ImageSrc = mostVisitedGroup.Group.AvatarSrc,
                                NotificationGroupId = notificationGroupId,
                                Text = $"W grupie {mostVisitedGroup.Group.Name} pojawił się wpis: {new string(post.TextContent.Take(30).ToArray())}...",
                                NotificationType = EnNotificationType.GP.ToString(),
                                Link = $"/group/{mostVisitedGroup.GroupId}",
                                CreatedAt = DateTime.Now,
                                UserId = userId
                            });
                    }
                    else if(groupPosts.Count > 2)
                    {
                        notifications.Add(new Models.Notification
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
