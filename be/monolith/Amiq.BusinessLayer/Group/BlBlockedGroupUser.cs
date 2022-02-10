using Amiq.BusinessLayer.Utils;
using Amiq.Contracts.Group;
using Amiq.DataAccessLayer.Group;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amiq.BusinessLayer.Group
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
