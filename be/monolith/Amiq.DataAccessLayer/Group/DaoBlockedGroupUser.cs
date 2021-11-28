using Amiq.Contracts.Group;
using Amiq.DataAccessLayer.Models.Models;
using Amiq.Mapping;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccessLayer.Group
{
    public class DaoBlockedGroupUser
    {
        private AmiqContext _amiqContext = new AmiqContext();

        public async Task<IEnumerable<DtoGroupBlockedUser>> GetGroupBlockedUsersAsync(int groupId)
        {
            return await _amiqContext.GroupBlockedUsers.Where(e => e.GroupId == groupId)
                .ProjectTo<DtoGroupBlockedUser>(APAutoMapper.Instance.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task BlockUserAsync(int userId, int groupId)
        {
            var entity = new GroupBlockedUser
            {
                UserId = userId,
                GroupId = groupId
            };
            _amiqContext.GroupBlockedUsers.Add(entity);
            var participant = _amiqContext.GroupParticipants.SingleOrDefault(g => g.UserId == userId && g.GroupId == groupId);
            if (participant != null)
            {
                _amiqContext.Remove(participant);
            }
            await _amiqContext.SaveChangesAsync();
        }
    }
}
