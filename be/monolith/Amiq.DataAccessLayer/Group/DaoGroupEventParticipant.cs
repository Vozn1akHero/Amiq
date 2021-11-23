using Amiq.Common.DbOperation;
using Amiq.Contracts.Group;
using Amiq.DataAccess.Models.Models;
using Amiq.Mapping;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccess.Group
{
    public class DaoGroupEventParticipant
    {
        private AmiqContext _amiqContext = new AmiqContext();

        public async Task JoinEventAsync(Guid groupParticipantId, Guid groupEventId)
        {
            _amiqContext.GroupEventParticipants.Add(new GroupEventParticipant
            {
                GroupParticipantId = groupParticipantId,
                GroupEventId = groupEventId
            });
            await _amiqContext.SaveChangesAsync();
        }

        public async Task LeaveGroupAsync(Guid groupParticipantId, Guid groupEventId)
        { 
            var entity = _amiqContext.GroupEventParticipants.Single(e => e.GroupParticipantId == groupParticipantId && e.GroupEventId == groupEventId);
            _amiqContext.Remove(entity);
            await _amiqContext.SaveChangesAsync();
        }

        public DtoGroupEventParticipant GetGroupEventParticipant(Guid groupParticipantId, Guid groupEventId)
        {
            return _amiqContext.GroupEventParticipants
                .Where(e => e.GroupParticipantId == groupParticipantId && e.GroupEventId == groupEventId)
                .ProjectTo<DtoGroupEventParticipant>(APAutoMapper.Instance.ConfigurationProvider)
                .SingleOrDefault();
        }
    }
}
