using AmicaPlus.DataAccess.Group;
using AmicaPlus.ResultSets.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmicaPlus.Business
{
    public class BsGroupParticipant
    {
        private DaGroupParticipant _daGroupParticipant;

        public BsGroupParticipant()
        {
            _daGroupParticipant = new DaGroupParticipant();
        }

        public async Task<List<RsGroup>> GetUserGroupsByUserIdAsync(int userId)
        {
            return await _daGroupParticipant.GetUserGroupsByUserIdAsync(userId);
        }

        public async Task LeaveGroupAsync(RsLeaveGroup rsLeaveGroup)
        {
            await _daGroupParticipant.LeaveGroupAsync(rsLeaveGroup);
        }

        public async Task JoinGroupAsync(RsJoinGroup rsJoinGroup)
        {
            await _daGroupParticipant.JoinGroupAsync(rsJoinGroup);
        }

        public async Task<RsGroupParticipant> GetGroupParticipantAsync(RsMinifiedGroupParticipant rsSimplifiedGroupParticipant)
        {
            return await _daGroupParticipant.GetGroupParticipantAsync(rsSimplifiedGroupParticipant);
        }
    }
}
