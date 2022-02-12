using Amiq.Services.Group.Contracts.Group;
using Amiq.Services.Group.DataAccessLayer.Models;
using Amiq.Services.Group.Mapping;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Amiq.Services.Group.DataAccessLayer
{
    public class DaoBlockedGroupUser
    {
        private AmiqGroupContext _amiqGroupContext = new AmiqGroupContext();

        public async Task<IEnumerable<DtoGroupBlockedUser>> GetGroupBlockedUsersAsync(int groupId)
        {
            return await _amiqGroupContext.GroupBlockedUsers.Where(e => e.GroupId == groupId)
                .ProjectTo<DtoGroupBlockedUser>(AmiqGroupAutoMapper.Instance.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task BlockUserAsync(int userId, int groupId)
        {
            var entity = new GroupBlockedUser
            {
                UserId = userId,
                GroupId = groupId
            };
            _amiqGroupContext.GroupBlockedUsers.Add(entity);
            var participant = _amiqGroupContext.GroupParticipants.SingleOrDefault(g => g.UserId == userId && g.GroupId == groupId);
            if (participant != null)
            {
                _amiqGroupContext.Remove(participant);
            }
            await _amiqGroupContext.SaveChangesAsync();
        }
    }
}
