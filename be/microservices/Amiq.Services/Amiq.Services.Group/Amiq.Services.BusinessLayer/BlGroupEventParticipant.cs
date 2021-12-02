using Amiq.Services.BusinessLayer.Base;
using Amiq.Services.BusinessLayer.Rules;
using Amiq.Services.Group.Contracts;
using Amiq.Services.Group.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.BusinessLayer
{
    public class BlGroupEventParticipant : BusinessLayerBase
    {
        private DaoGroupEventParticipant _daoGroupEventParticipant = new DaoGroupEventParticipant();
        private DaoGroupEvent _daGroupEvent = new DaoGroupEvent();
        private DaoGroupParticipant _daGroupParticipant = new DaoGroupParticipant();

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
