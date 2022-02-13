using Amiq.Services.Common;
using Amiq.Services.Common.Contracts;
using Amiq.Services.Group.Contracts.Group;
using Amiq.Services.Group.DataAccessLayer.Models;
using Amiq.Services.Group.Mapping;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Amiq.Services.Group.DataAccessLayer
{
    public class DaoGroupEvent
    {
        private AmiqGroupContext _amiqGroupContext = new AmiqGroupContext();

        public async Task<DtoListResponseOf<DtoGroupEvent>> GetAllGroupEventsAsync(int groupId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            var result = new DtoListResponseOf<DtoGroupEvent>();
            IQueryable query = _amiqGroupContext.GroupEvents.AsNoTracking()
                .Include(e=>e.GroupEventParticipants)
                .Where(x => x.GroupId == groupId)
                .Paginate(dtoPaginatedRequest.Page, dtoPaginatedRequest.Count);
            result.Entities = await AmiqGroupAutoMapper.Instance.ProjectTo<DtoGroupEvent>(query).ToListAsync();
            result.Length = await _amiqGroupContext.GroupEvents.AsNoTracking()
                .Where(x => x.GroupId == groupId).CountAsync();
            return result;
        }

        public DtoGroup GetGroupByGroupEventId(Guid groupEventId)
        {
            return _amiqGroupContext
                .Groups
                .Where(e => e.GroupEvents.Any(d => d.GroupEventId == groupEventId))
                .ProjectTo<DtoGroup>(AmiqGroupAutoMapper.Instance.ConfigurationProvider)
                .Single();
        }

        public DtoGroupEvent GetEventByIdAsync(Guid groupEventId)
        {
            return _amiqGroupContext.GroupEvents.AsNoTracking()
                .Include(e => e.GroupEventParticipants)
                .Where(x => x.GroupEventId == groupEventId)
                .ProjectTo<DtoGroupEvent>(AmiqGroupAutoMapper.Instance.ConfigurationProvider)
                .Single();
        }

        public bool IsParticipant(int userId, Guid groupEventId)
        {
            return _amiqGroupContext.GroupEvents
                .AsNoTracking()
                .Any(e=>e.GroupEventId == groupEventId
                    && e.GroupEventParticipants.Any(d=>d.GroupParticipant.UserId == userId));
        }

        public GroupEvent GetEventById(Guid groupEventId) => _amiqGroupContext.GroupEvents
            .SingleOrDefault(e => e.GroupEventId == groupEventId);

        public async Task CancelAsync(GroupEvent entity)
        {
            entity.IsCancelled = true;
            await _amiqGroupContext.SaveChangesAsync();
        }

        public async Task<DtoEditEntityResponse> EditAsync(DtoGroupEvent dtoGroupEvent)
        {
            var result = new DtoEditEntityResponse();



            return result;
        }

        public async Task<DtoDeleteEntityResponse> Delete(GroupEvent groupEvent)
        {
            DtoDeleteEntityResponse res = new();
            try
            {
                _amiqGroupContext.GroupEvents.Remove(groupEvent);
                await _amiqGroupContext.SaveChangesAsync();
                res.IsBusinessException = false;
                res.Entity = AmiqGroupAutoMapper.Instance.Map<DtoGroupEvent>(groupEvent);
            }
            catch (Exception ex)
            {
                res.IsBusinessException = true;
                res.BusinessException = ex.Message;
            }
            return res;
        }

        public async Task<DtoEditEntityResponse> CancelEventAsync(GroupEvent groupEvent)
        {
            if(groupEvent == null) throw new ArgumentNullException(nameof(groupEvent));
            DtoEditEntityResponse result = new();
            try
            {
                groupEvent.IsCancelled = true;
                _amiqGroupContext.Entry(groupEvent).State = EntityState.Modified;
                await _amiqGroupContext.SaveChangesAsync();
                result.Result = true;
                result.Entity = AmiqGroupAutoMapper.Instance.Map<DtoGroupEvent>(groupEvent);
            } catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<DtoEditEntityResponse> ReopenEventAsync(GroupEvent groupEvent)
        {
            if (groupEvent == null) throw new ArgumentNullException(nameof(groupEvent));
            DtoEditEntityResponse result = new();
            try
            {
                groupEvent.IsCancelled = false;
                _amiqGroupContext.Entry(groupEvent).State = EntityState.Modified;
                await _amiqGroupContext.SaveChangesAsync();
                result.Result = true;
                result.Entity = AmiqGroupAutoMapper.Instance.Map<DtoGroupEvent>(groupEvent);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<DtoEditEntityResponse> SetEventVisibilityAsync(GroupEvent groupEvent, bool isVisible)
        {
            if (groupEvent == null) throw new ArgumentNullException(nameof(groupEvent));
            DtoEditEntityResponse result = new();
            try
            {
                groupEvent.IsHidden = !isVisible;
                _amiqGroupContext.Entry(groupEvent).State = EntityState.Modified;
                await _amiqGroupContext.SaveChangesAsync();
                result.Result = true;
                result.Entity = AmiqGroupAutoMapper.Instance.Map<DtoGroupEvent>(groupEvent);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
