using Amiq.Business.Utils;
using Amiq.Contracts;
using Amiq.Contracts.Group;
using Amiq.Contracts.Utils;
using Amiq.DataAccess.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business.Group
{
    public class BsGroupEvent : BusinessLayerBase
    {
        private DaGroupEvent _daGroupEvent = new DaGroupEvent();

        public async Task<DtoListResponseOf<DtoGroupEvent>> GetAllGroupEventsAsync(int groupId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            return await _daGroupEvent.GetAllGroupEventsAsync(groupId, dtoPaginatedRequest);
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
