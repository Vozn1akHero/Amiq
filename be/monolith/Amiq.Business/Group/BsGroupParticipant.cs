using Amiq.Contracts.Group;
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

        public async Task<List<DtoGroup>> GetUserGroupsByUserIdAsync(int userId)
        {
            return await _daGroupParticipant.GetUserGroupsByUserIdAsync(userId);
        }

        public async Task LeaveGroupAsync(DtoLeaveGroup dtoLeaveGroup)
        {
            await _daGroupParticipant.LeaveGroupAsync(dtoLeaveGroup);
        }

        public async Task JoinGroupAsync(DtoJoinGroup dtoJoinGroup)
        {
            await _daGroupParticipant.JoinGroupAsync(dtoJoinGroup);
        }

        public async Task<DtoGroupParticipant> GetGroupParticipantAsync(DtoMinifiedGroupParticipant dtoSimplifiedGroupParticipant)
        {
            return await _daGroupParticipant.GetGroupParticipantAsync(dtoSimplifiedGroupParticipant);
        }
    }
}
