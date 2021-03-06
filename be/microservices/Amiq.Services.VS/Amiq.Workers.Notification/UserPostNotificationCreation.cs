using Amiq.Services.Common.Enums;
using Amiq.Workers.Notification.Models;
using Microsoft.EntityFrameworkCore;

namespace Amiq.Workers.Notification
{
    public class UserPostNotificationCreation : NotificationCreationStrategy
    {
        public override IEnumerable<Models.Notification> Create(IEnumerable<UserNotificationsQueue> users)
        {
            List<Models.Notification> result = new();

            List<int> userIds = users.Select(e=>e.UserId).ToList();
            var mostVisitedProfilesInBulk = DbContext.ProfileVisitations.AsNoTracking()
                .Where(e => userIds.Contains(e.UserId)).OrderBy(e=>e.UserId).ToList();
            Dictionary<int, List<ProfileVisitation>> mostVisitedProfilesGrouped = mostVisitedProfilesInBulk
                .Select((x) => new { Index = x.UserId, Value = x })
                .GroupBy(e => e.Index)
                .Select(e => new KeyValuePair<int, List<ProfileVisitation>>(e.Select(v => v.Index).First(), e.Select(v => v.Value).ToList()))
                .ToDictionary(e=>e.Key, e => e.Value);

            foreach (var userProfileVisitations in mostVisitedProfilesGrouped)
            {
                int userId = userProfileVisitations.Key;
                Guid notificationGroupId = users.Single(e => e.UserId == userId).NotificationGroupId;
                var mostVisitedProfileUserIds = userProfileVisitations.Value.Select(e => e.VisitedUserId).ToHashSet();

                var activeFriendships = DbContext.Friendships.AsNoTracking().Where(e => e.FirstUserId == userId || e.SecondUserId == userId)
                    .Where(e => e.FirstUserId == userId 
                        ? mostVisitedProfileUserIds.Contains(e.SecondUserId) : mostVisitedProfileUserIds.Contains(e.FirstUserId))
                    .Select(e => new { e.FirstUserId, e.SecondUserId })
                    .ToList();
                var filteredVisitations = new List<ProfileVisitation>();
                foreach (var activeFriendship in activeFriendships)
                {
                    var mostVisitedProfileEntity = userProfileVisitations.Value
                        .Where(e => (e.UserId == activeFriendship.FirstUserId && e.VisitedUserId == activeFriendship.SecondUserId)
                            || (e.UserId == activeFriendship.SecondUserId && e.VisitedUserId == activeFriendship.FirstUserId))
                        .SingleOrDefault();
                    if (mostVisitedProfileEntity != null)
                        filteredVisitations.Add(mostVisitedProfileEntity);
                }

                if (!filteredVisitations.Any()) continue;

                var userPostsInBulk = new List<UserPost>();
                foreach (var filteredVisitation in filteredVisitations)
                {
                    userPostsInBulk.AddRange(DbContext.UserPosts.AsNoTracking()
                        .Where(e => filteredVisitation.VisitedUserId == e.UserId
                            && e.CreatedAt > filteredVisitation.LastVisited)
                        .OrderBy(e => e.UserId)
                        .ThenByDescending(e => e.CreatedAt)
                        .Include(e=>e.User)
                        .ToList());
                }
                Dictionary<User, List<UserPost>> userPostsGrouped = userPostsInBulk
                    .Select((x) => new { Index = x.User, Value = x })
                    .GroupBy(e => e.Index.UserId)
                    .Select(e => new KeyValuePair<User, List<UserPost>>(e.Select(v => v.Value.User).First(), e.Select(v => v.Value).ToList()))
                    .ToDictionary(e => e.Key, e => e.Value);
                foreach(KeyValuePair<User, List<UserPost>> userPosts in userPostsGrouped){
                    if(userPosts.Value.Count > 0 && userPosts.Value.Count <= 2)
                    {
                        result.Add(new Models.Notification
                        {
                            ImageSrc = userPosts.Key.AvatarPath,
                            NotificationGroupId = notificationGroupId,
                            Text = $"Użytkownik {userPosts.Key.Name + " " + userPosts.Key.Surname} dodał nowy wpis: i inne",
                            Link = $"/profile/{userPosts.Key.UserId}",
                            NotificationType = EnNotificationType.UP.ToString(),
                            CreatedAt = DateTime.Now,
                            UserId = userId
                        });
                    }
                    else if(userPosts.Value.Count > 2)
                    {
                        result.Add(new Models.Notification
                        {
                            ImageSrc = userPosts.Key.AvatarPath,
                            NotificationGroupId = notificationGroupId,
                            Text = $"Użytkownik {userPosts.Key.Name + " " + userPosts.Key.Surname} dodał {userPosts.Value.Count} nowych wpisów",
                            Link = $"/profile/{userPosts.Key.UserId}",
                            NotificationType = EnNotificationType.UP.ToString(),
                            CreatedAt = DateTime.Now,
                            UserId = userId
                        });
                    }
                }
            }

            return result;
        }
    }
}