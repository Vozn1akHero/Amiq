using Amiq.Services.Common.Contracts;
using Amiq.Services.Group.BusinessLayer.Base;
using Amiq.Services.Group.Contracts.Group;
using Amiq.Services.Group.DataAccessLayer;

namespace Amiq.Services.Group.BusinessLayer
{
    public class BlGroupEvent : BusinessLayerBase
    {
        private DaoGroupEvent _daGroupEvent = new DaoGroupEvent();

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

        public async Task<DtoEditEntityResponse> CancelEventAsync(int groupId, Guid groupEventId)
        {
            //CheckBsRule(new );
            var entity = _daGroupEvent.GetEventById(groupEventId);
            return await _daGroupEvent.CancelEventAsync(entity);
        }

        public async Task<DtoEditEntityResponse> ReopenEventAsync(int groupId, Guid groupEventId)
        {
            //CheckBsRule(new );
            var entity = _daGroupEvent.GetEventById(groupEventId);
            return await _daGroupEvent.ReopenEventAsync(entity);
        }

        public async Task<DtoEditEntityResponse> SetEventVisibilityAsync(int groupId, Guid groupEventId, bool isVisible)
        {
            //CheckBsRule(new );
            var entity = _daGroupEvent.GetEventById(groupEventId);
            return await _daGroupEvent.SetEventVisibilityAsync(entity, isVisible);
        }
    }
}
