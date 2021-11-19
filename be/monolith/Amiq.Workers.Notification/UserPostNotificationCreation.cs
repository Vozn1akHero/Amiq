using Amiq.DataAccess.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Workers.Notification
{
    public class UserPostNotificationCreation : NotificationCreationStrategy
    {
        /// <summary>
        /// Metoda zwracająca nowe wpisy użytkowników
        /// 
        /// Reguły biznesowe:
        /// 1. Uwzględnienie aktywnych znajomości
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public override IEnumerable<DataAccess.Models.Models.Notification> Create(IEnumerable<int> userIds)
        {
            List<DataAccess.Models.Models.Notification> result = new();

            var mostVisitedProfilesInBulk = DbContext.ProfileVisitations.AsNoTracking().Where(e => userIds.Contains(e.UserId)).OrderBy(e=>e.UserId).ToList();
            Dictionary<int, List<ProfileVisitation>> mostVisitedProfilesGrouped = mostVisitedProfilesInBulk
                .Select((x) => new { Index = x.UserId, Value = x })
                .GroupBy(e => e.Index)
                .Select(e => new KeyValuePair<int, List<ProfileVisitation>>(e.Select(v => v.Index).First(), e.Select(v => v.Value).ToList()))
                .ToDictionary(e=>e.Key, e => e.Value);

            foreach (var userProfileVisitations in mostVisitedProfilesGrouped)
            {
                int userId = userProfileVisitations.Key;
                var mostVisitedProfileUserIds = userProfileVisitations.Value.Select(e => e.VisitedUserId).ToHashSet();

                // uwzlędnienie tylko aktywnych znajomości
                var activeFriendships = DbContext.Friendships.AsNoTracking().Where(e => e.FirstUserId == userId || e.SecondUserId == userId)
                    .Where(e => e.FirstUserId == userId ? mostVisitedProfileUserIds.Contains(e.SecondUserId) : mostVisitedProfileUserIds.Contains(e.FirstUserId))
                    .Select(e => new { e.FirstUserId, e.SecondUserId })
                    .ToList();
                var filteredVisitations = new List<ProfileVisitation>();
                foreach (var activeFriendship in activeFriendships)
                {
                    var mostVisitedProfileEntity = userProfileVisitations.Value.Where(e => (e.UserId == activeFriendship.FirstUserId && e.VisitedUserId == activeFriendship.SecondUserId)
                        || (e.UserId == activeFriendship.SecondUserId && e.VisitedUserId == activeFriendship.FirstUserId)).SingleOrDefault();
                    if (mostVisitedProfileEntity != null)
                        filteredVisitations.Add(mostVisitedProfileEntity);
                }

                var userPostsInBulk = DbContext.UserPosts.AsNoTracking()
                        .Where(e => filteredVisitations.Select(e => e.VisitedUserId).Contains(e.UserId)
                            && e.Post.CreatedAt > filteredVisitations.Single(d => d.VisitedUserId == e.UserId).LastVisited)
                        .OrderBy(e=>e.UserId)
                        .ThenByDescending(e=>e.Post.CreatedAt)
                        .ToList();
                Dictionary<User, List<UserPost>> userPostsGrouped = userPostsInBulk
                    .Select((x) => new { Index = x.User, Value = x })
                    .GroupBy(e => e.Index.UserId)
                    .Select(e => new KeyValuePair<User, List<UserPost>>(e.Select(v => v.Value.User).First(), e.Select(v => v.Value).ToList()))
                    .ToDictionary(e => e.Key, e => e.Value);
                foreach(KeyValuePair<User, List<UserPost>> userPosts in userPostsGrouped){
                    if(userPosts.Value.Count > 0 && userPosts.Value.Count <= 2)
                    {
                        result.Add(new DataAccess.Models.Models.Notification
                        {
                            ImageSrc = userPosts.Key.AvatarPath,
                            Text = $"Użytkownik <b>{userPosts.Key.Name + userPosts.Key.Surname}</b> dodał nowy wpis: i <b>inne</b>",
                            Link = $"/profile/{userPosts.Key.UserId}",
                            CreatedAt = DateTime.Now,
                            UserId = userId
                        });
                    }
                    else if(userPosts.Value.Count > 2)
                    {
                        result.Add(new DataAccess.Models.Models.Notification
                        {
                            ImageSrc = userPosts.Key.AvatarPath,
                            Text = $"Użytkownik <b>{userPosts.Key.Name + userPosts.Key.Surname}</b> dodał {userPosts.Value.Count} nowych wpisów",
                            Link = $"/profile/{userPosts.Key.UserId}",
                            CreatedAt = DateTime.Now,
                            UserId = userId
                        });
                    }
                }
            }

            /*foreach (int userId in userIds)
            {
                var mostVisitedProfiles = DbContext.ProfileVisitations.AsNoTracking().Where(e => e.UserId == userId)
                        .Take(5)
                        .OrderByDescending(e => e.VisitationTotalTime)
                        .ToList();

                // uwzlędnienie tylko aktywnych znajomości
                var mostVisitedProfileUserIds = mostVisitedProfiles.Select(e => e.VisitedUserId).ToHashSet();
                var activeFriendships = DbContext.Friendships.AsNoTracking().Where(e => e.FirstUserId == userId || e.SecondUserId == userId)
                    .Where(e => e.FirstUserId == userId ? mostVisitedProfileUserIds.Contains(e.SecondUserId) : mostVisitedProfileUserIds.Contains(e.FirstUserId))
                    .Select(e=>new { e.FirstUserId, e.SecondUserId })
                    .ToList();
                var filteredVisitations = new List<ProfileVisitation>();
                foreach(var activeFriendship in activeFriendships)
                {
                    var mostVisitedProfileEntity = mostVisitedProfiles.Where(e => (e.UserId == activeFriendship.FirstUserId && e.VisitedUserId == activeFriendship.SecondUserId)
                        || (e.UserId == activeFriendship.SecondUserId && e.VisitedUserId == activeFriendship.FirstUserId)).SingleOrDefault();
                    if (mostVisitedProfileEntity != null)
                        filteredVisitations.Add(mostVisitedProfileEntity);
                }

                var userPostsInBulk = DbContext.UserPosts.AsNoTracking()
                        .Where(e => filteredVisitations.Select(e=>e.VisitedUserId).Contains(e.UserId)
                            && e.Post.CreatedAt > filteredVisitations.Single(d=>d.VisitedUserId==e.UserId).LastVisited)
                        .ToList();

                
            }*/

            return result;
        }
    }
}
