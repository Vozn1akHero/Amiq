using Amiq.Services.BusinessLayer.Base;
using Amiq.Services.Group.Contracts;
using Amiq.Services.Group.DataAccessLayer;

namespace Amiq.Services.BusinessLayer
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
