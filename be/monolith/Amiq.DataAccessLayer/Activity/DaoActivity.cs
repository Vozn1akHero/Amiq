using Amiq.Contracts.Activity;
using Amiq.DataAccessLayer.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccessLayer.Activity
{
    public class DaoActivity
    {
        private AmiqContext _amiqContext = new AmiqContext();

        public async Task CreateAsync(int userId, DtoCreateActivityInBulk dtoCreateActivityInBulk)
        {
            var groupVisitations = new List<GroupVisitation>();
            var profileVisitations = new List<ProfileVisitation>();

            var visitedGroupIds = dtoCreateActivityInBulk.GroupVisitations.Select(e=>e.GroupId).ToList();
            var visitedProfileIds = dtoCreateActivityInBulk.ProfileVisitations.Select(e=>e.VisitedUserId).ToList();

            var existingVisitedGroups = _amiqContext.GroupVisitations.AsNoTracking()
                .Where(e=>e.UserId == userId && visitedGroupIds.Contains(e.GroupId))
                .ToList();
            var existingVisitedProfiles = _amiqContext.ProfileVisitations.AsNoTracking()
                .Where(e => e.UserId == userId && visitedGroupIds.Contains(e.VisitedUserId))
                .ToList();

            foreach (var dtoGroupVisitation in dtoCreateActivityInBulk.GroupVisitations)
            {
                var existingVisitedGroup = existingVisitedGroups.SingleOrDefault(e => e.GroupId == dtoGroupVisitation.GroupId);
                long nextVisitationTotalTime = existingVisitedGroup != null ? existingVisitedGroup.VisitationTotalTime + dtoGroupVisitation.VisitationTotalTime
                    : dtoGroupVisitation.VisitationTotalTime;
                groupVisitations.Add(new GroupVisitation { 
                    UserId = userId,
                    GroupId = dtoGroupVisitation.GroupId,
                    LastVisited = dtoGroupVisitation.LastVisited,
                    VisitationTotalTime = nextVisitationTotalTime
                });
            }
            foreach (var dtoProfileVisitation in dtoCreateActivityInBulk.ProfileVisitations)
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
