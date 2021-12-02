using Amiq.Services.BusinessLayer.Base;
using Amiq.Services.Group.Contracts;
using Amiq.Services.Group.Contracts.Utils;
using Amiq.Services.Group.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.BusinessLayer
{
    public class BlGroup : BusinessLayerBase
    {
        private DaoGroup _daGroup = new DaoGroup();
        private DaoGroupParticipant _daGroupParticipant = new DaoGroupParticipant();

        public async Task<DtoGroupCard> CreateGroupAsync(int creatorId, DtoCreateGroup dtoCreateGroup)
        {
            return await _daGroup.CreateGroupAsync(creatorId, dtoCreateGroup);
        }

        public async Task<DtoGroupUserParams> GetGroupUserParamsAsync(int userId, int groupId)
        {
            return await _daGroupParticipant.GetGroupUserParamsAsync(userId, groupId);
        }

        public DtoGroup GetGroupDataById(int groupId)
        {
            var group = new DtoGroup();
            return group;
        }

        public async Task<IEnumerable<DtoGroup>> GetParticipatedGroupByUserId(int userId)
        {
            var output = new List<DtoGroup>();

            return output;
        }

        public async Task<IEnumerable<DtoGroupCard>> GetByName(int userId, string name)
        {
            return await _daGroup.GetByNameAsync(userId, name);
        }

        public async Task<DtoGroup> GetGroupById(int groupId)
        {
            return await _daGroup.GetGroupById(groupId);
        }

        public async Task<DtoEditEntityResponse> EditAsync(DtoEditGroupDataRequest dtoEditGroupDataRequest)
        {
            return await _daGroup.EditAsync(dtoEditGroupDataRequest);
        }
    }
}
