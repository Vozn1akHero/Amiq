using Amiq.Business.Group.Rules;
using Amiq.Business.Utils;
using Amiq.Common.DbOperation;
using Amiq.Contracts.Group;
using Amiq.DataAccess.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business.Group
{
    public class BlGroupEventParticipant : BusinessLayerBase
    {
        private DaoGroupEventParticipant _daoGroupEventParticipant = new DaoGroupEventParticipant();
        private DaGroupEvent _daGroupEvent = new DaGroupEvent();
        private DaGroupParticipant _daGroupParticipant = new DaGroupParticipant();

        public async Task<DtoGroupEventParticipant> JoinEventAsync(int userId, Guid groupEventId)
        {
            var group = _daGroupEvent.GetGroupByGroupEventId(groupEventId);
            CheckBsRule(new ActionCanOnlyBePerformedByGroupParticipant(_daGroupParticipant, userId, group.GroupId));
            var groupParticipant = _daGroupParticipant.GetGroupParticipant(userId, group.GroupId);
            await _daoGroupEventParticipant.JoinEventAsync(groupParticipant.GroupParticipantId, groupEventId);
            return _daoGroupEventParticipant.GetGroupEventParticipant(groupParticipant.GroupParticipantId, groupEventId);
        }

        public async Task LeaveGroupAsync(int userId, Guid groupEventId)
        {
            var group = _daGroupEvent.GetGroupByGroupEventId(groupEventId);
            CheckBsRule(new ActionCanOnlyBePerformedByGroupParticipant(_daGroupParticipant, userId, group.GroupId));
            var groupParticipant = _daGroupParticipant.GetGroupParticipant(userId, group.GroupId);
            await _daoGroupEventParticipant.LeaveGroupAsync(groupParticipant.GroupParticipantId, groupEventId);
        }
    }
}
