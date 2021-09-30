using Amiq.Contracts.Group;
using Amiq.Contracts.Utils;
using Amiq.DataAccess.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business
{
    public class BsGroupParticipant
    {
        private DaGroupParticipant _daGroupParticipant;

        public BsGroupParticipant()
        {
            _daGroupParticipant = new DaGroupParticipant();
        }

        public async Task<List<DtoGroup>> GetUserGroupsByUserIdAsync(int userId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            return await _daGroupParticipant.GetUserGroupsByUserIdAsync(userId, dtoPaginatedRequest);
        }

        public async Task LeaveGroupAsync(int userId, int groupId)
        {
            await _daGroupParticipant.LeaveGroupAsync(userId, groupId);
        }

        public async Task JoinGroupAsync(DtoJoinGroup dtoJoinGroup)
        {
            await _daGroupParticipant.JoinGroupAsync(dtoJoinGroup);
        }

        public async Task<DtoGroupParticipant> GetGroupParticipantAsync(DtoMinifiedGroupParticipant dtoSimplifiedGroupParticipant)
        {
            return await _daGroupParticipant.GetGroupParticipantAsync(dtoSimplifiedGroupParticipant);
        }

        public async Task<DtoGroupViewer> GetGroupViewerByUserIdAsync(int userId, int groupId)
        {
            var result = await _daGroupParticipant.GetGroupViewerByUserIdAsync(userId, groupId);
            return result;
        }
    }
}
