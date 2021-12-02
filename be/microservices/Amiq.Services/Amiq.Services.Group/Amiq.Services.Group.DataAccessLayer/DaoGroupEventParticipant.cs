using Amiq.Services.Group.Contracts;
using Amiq.Services.Group.DataAccessLayer.Models.Models;
using Amiq.Services.Group.Mapping;
using AutoMapper.QueryableExtensions;

namespace Amiq.Services.Group.DataAccessLayer
{
    public class DaoGroupEventParticipant
    {
        private AmiqGroupContext _amiqGroupContext = new AmiqGroupContext();

        public async Task JoinEventAsync(Guid groupParticipantId, Guid groupEventId)
        {
            _amiqGroupContext.GroupEventParticipants.Add(new GroupEventParticipant
            {
                GroupParticipantId = groupParticipantId,
                GroupEventId = groupEventId
            });
            await _amiqGroupContext.SaveChangesAsync();
        }

        public async Task LeaveGroupAsync(Guid groupParticipantId, Guid groupEventId)
        { 
            var entity = _amiqGroupContext.GroupEventParticipants.Single(e => e.GroupParticipantId == groupParticipantId && e.GroupEventId == groupEventId);
            _amiqGroupContext.Remove(entity);
            await _amiqGroupContext.SaveChangesAsync();
        }

        public DtoGroupEventParticipant GetGroupEventParticipant(Guid groupParticipantId, Guid groupEventId)
        {
            return _amiqGroupContext.GroupEventParticipants
                .Where(e => e.GroupParticipantId == groupParticipantId && e.GroupEventId == groupEventId)
                .ProjectTo<DtoGroupEventParticipant>(AmiqGroupAutoMapper.Instance.ConfigurationProvider)
                .SingleOrDefault();
        }
    }
}
