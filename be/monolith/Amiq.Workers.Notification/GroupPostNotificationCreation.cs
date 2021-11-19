using Amiq.DataAccess.Models.Models;
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
        public override IEnumerable<DataAccess.Models.Models.Notification> Create(IEnumerable<int> userIds)
        {
            List<DataAccess.Models.Models.Notification> notifications = new();

            foreach(int userId in userIds)
            {
                var mostVisitedGroups = DbContext.GroupVisitations.AsNoTracking()
                        .Where(e => e.UserId == userId)
                        .Take(5)
                        .OrderByDescending(e => e.VisitationTotalTime)
                        .ToList();
                foreach (var mostVisitedGroup in mostVisitedGroups)
                {
                    var groupPosts = DbContext.GroupPosts
                        .AsNoTracking()
                        .Where(e => e.Post.CreatedAt > mostVisitedGroup.LastVisited
                            && e.GroupId == mostVisitedGroup.GroupId)
                        .ToList();

                    if(groupPosts.Count > 0 && groupPosts.Count <= 2)
                    {
                        foreach(var post in groupPosts)
                            notifications.Add(new DataAccess.Models.Models.Notification {
                                ImageSrc = mostVisitedGroup.Group.AvatarSrc,
                                Text = $"W grupie <b>{mostVisitedGroup.Group.Name}</b> pojawił się wpis: {post.Post.TextContent.Substring(0, 30)}...",
                                Link = $"/group/{mostVisitedGroup.GroupId}",
                                CreatedAt = DateTime.Now,
                                UserId = userId
                            });
                    }
                    else if(groupPosts.Count > 2)
                    {
                        notifications.Add(new DataAccess.Models.Models.Notification
                        {
                            ImageSrc = mostVisitedGroup.Group.AvatarSrc,
                            Text = $"W grupie <b>{mostVisitedGroup.Group.Name}</b> pojawiło się {groupPosts.Count} wpisów",
                            Link = $"/group/{mostVisitedGroup.GroupId}",
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
