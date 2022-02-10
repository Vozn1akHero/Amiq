using Amiq.Services.Notification.Contracts.Activity;
using Amiq.Services.Notification.DataAccessLayer.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Amiq.Services.Notification.DataAccessLayer
{
    public class DaoActivity
    {
        private AmiqNotificationContext _amiqContext = new AmiqNotificationContext();

        public async Task CreateAsync(int userId, DtoCreateActivityInBulk dtoCreateActivityInBulk)
        {
            var groupVisitations = new List<GroupVisitation>();
            var profileVisitations = new List<ProfileVisitation>();

            var visitedGroupIds = dtoCreateActivityInBulk.GroupVisitations?.Select(e => e.GroupId).ToList();
            var visitedProfileIds = dtoCreateActivityInBulk.UserProfileVisitations?.Select(e => e.VisitedUserId).ToList();

            var existingVisitedGroups = _amiqContext.GroupVisitations.AsNoTracking()
                .Where(e => e.UserId == userId && visitedGroupIds.Contains(e.GroupId))
                .ToList();
            var existingVisitedProfiles = _amiqContext.ProfileVisitations.AsNoTracking()
                .Where(e => e.UserId == userId && visitedGroupIds.Contains(e.VisitedUserId))
                .ToList();

            foreach (var dtoGroupVisitation in dtoCreateActivityInBulk.GroupVisitations)
            {
                var existingVisitedGroup = existingVisitedGroups.SingleOrDefault(e => e.GroupId == dtoGroupVisitation.GroupId);
                long nextVisitationTotalTime = existingVisitedGroup != null ? existingVisitedGroup.VisitationTotalTime + dtoGroupVisitation.VisitationTotalTime
                    : dtoGroupVisitation.VisitationTotalTime;
                groupVisitations.Add(new GroupVisitation
                {
                    UserId = userId,
                    GroupId = dtoGroupVisitation.GroupId,
                    LastVisited = dtoGroupVisitation.LastVisited,
                    VisitationTotalTime = nextVisitationTotalTime
                });
            }
            foreach (var dtoProfileVisitation in dtoCreateActivityInBulk.UserProfileVisitations)
            {
                var existingVisitedProfile = existingVisitedProfiles.SingleOrDefault(e => e.VisitedUserId == dtoProfileVisitation.VisitedUserId);
                long nextVisitationTotalTime = existingVisitedProfile != null ? existingVisitedProfile.VisitationTotalTime + dtoProfileVisitation.VisitationTotalTime
                    : dtoProfileVisitation.VisitationTotalTime;
                profileVisitations.Add(new ProfileVisitation
                {
                    UserId = userId,
                    VisitedUserId = dtoProfileVisitation.VisitedUserId,
                    LastVisited = dtoProfileVisitation.LastVisited,
                    VisitationTotalTime = nextVisitationTotalTime
                });
            }
            _amiqContext.RemoveRange(existingVisitedGroups);
            _amiqContext.RemoveRange(existingVisitedProfiles);
            _amiqContext.AddRange(profileVisitations);
            _amiqContext.AddRange(groupVisitations);
            await _amiqContext.SaveChangesAsync();
        }
    }
}
