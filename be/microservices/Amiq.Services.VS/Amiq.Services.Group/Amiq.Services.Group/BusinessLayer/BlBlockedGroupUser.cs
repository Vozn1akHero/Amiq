using Amiq.Services.Group.BusinessLayer.Base;
using Amiq.Services.Group.Contracts.Group;
using Amiq.Services.Group.DataAccessLayer;

namespace Amiq.Services.Group.BusinessLayer
{
    public class BlBlockedGroupUser : BusinessLayerBase
    {
        private DaoBlockedGroupUser _daoBlockedGroupUser = new DaoBlockedGroupUser();

        public async Task BlockUserAsync(int userId, int groupId)
        {
            await _daoBlockedGroupUser.BlockUserAsync(userId, groupId);
        }

        public async Task<IEnumerable<DtoGroupBlockedUser>> GetGroupBlockedUsersAsync(int groupId)
        {
            return await _daoBlockedGroupUser.GetGroupBlockedUsersAsync(groupId);
        }
    }
}
