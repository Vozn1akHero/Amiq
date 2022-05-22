using Amiq.BusinessLayer.Group.Rules;
using Amiq.BusinessLayer.Utils;
using Amiq.Contracts.Group;
using Amiq.Contracts.Utils;
using Amiq.DataAccessLayer.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.BusinessLayer.GroupEvent
{
    public class BlGroupEvent : BusinessLayerBase
    {
        private readonly DaoGroupEvent _daGroupEvent = new DaoGroupEvent();
        private readonly DaoGroupParticipant _daoGroupParticipant = new DaoGroupParticipant();

        public async Task<DtoListResponseOf<DtoGroupEvent>> GetAllGroupEventsAsync(int groupId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            return await _daGroupEvent.GetAllGroupEventsAsync(groupId, dtoPaginatedRequest);
        }

        public DtoGroupEvent GetEventByIdAsync(int requestCreatorId, Guid groupEventId)
        {
            DtoGroupEvent groupEvent = _daGroupEvent.GetEventByIdAsync(groupEventId);
            groupEvent.IsRequestCreatorParticipant = _daGroupEvent.IsParticipant(requestCreatorId, groupEventId);
            return groupEvent;
        }

        public async Task<DtoEditEntityResponse> CancelEventAsync(int userId, int groupId, Guid groupEventId)
        {
            await CheckBsRuleAsync(new MustBeAdminRule(_daoGroupParticipant, userId, groupId));
            var entity = _daGroupEvent.GetEventById(groupEventId);
            return await _daGroupEvent.CancelEventAsync(entity);
        }

        public async Task<DtoEditEntityResponse> ReopenEventAsync(int userId, int groupId, Guid groupEventId)
        {
            await CheckBsRuleAsync(new MustBeAdminRule(_daoGroupParticipant, userId, groupId));
            var entity = _daGroupEvent.GetEventById(groupEventId);
            return await _daGroupEvent.ReopenEventAsync(entity);
        }

        public async Task<DtoEditEntityResponse> SetEventVisibilityAsync(int userId, int groupId, Guid groupEventId, bool isVisible)
        {
            await CheckBsRuleAsync(new MustBeAdminRule(_daoGroupParticipant, userId, groupId));
            var entity = _daGroupEvent.GetEventById(groupEventId);
            return await _daGroupEvent.SetEventVisibilityAsync(entity, isVisible);
        }
    }
}
