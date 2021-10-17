using Amiq.Contracts;
using Amiq.Contracts.Group;
using Amiq.Contracts.Utils;
using Amiq.DataAccess.Models.Models;
using Amiq.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccess.Group
{
    public class DaGroupEvent
    {
        private AmiqContext _context = new AmiqContext();

        public async Task<DtoListResponseOf<DtoGroupEvent>> GetAllGroupEventsAsync(int groupId)
        {
            var result = new DtoListResponseOf<DtoGroupEvent>();
            var query = _context.GroupEvents.AsNoTracking()
                .Include(e=>e.GroupEventParticipants)
                .Where(x => x.GroupId == groupId);
            //result.Length = await _context.GroupEvents.AsNoTracking().Where(x => x.GroupId == groupId).CountAsync();
            var data = await APAutoMapper.Instance.ProjectTo<DtoGroupEvent>(query).ToListAsync();
            result.Entities = data;
            result.Length = data.Count();
            return result;
        }

        public GroupEvent GetEventById(Guid groupEventId) => _context.GroupEvents
            .SingleOrDefault(e => e.GroupEventId == groupEventId);

        public async Task CancelAsync(GroupEvent entity)
        {
            entity.IsCancelled = true;
            await _context.SaveChangesAsync();
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
                _context.GroupEvents.Remove(groupEvent);
                await _context.SaveChangesAsync();
                res.Result = true;
                res.Entity = APAutoMapper.Instance.Map<DtoGroupEvent>(groupEvent);
            }
            catch (Exception ex)
            {
                res.Result = false;
                res.Message = ex.Message;
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
                _context.Entry(groupEvent).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                result.Result = true;
                result.Entity = APAutoMapper.Instance.Map<DtoGroupEvent>(groupEvent);
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
                _context.Entry(groupEvent).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                result.Result = true;
                result.Entity = APAutoMapper.Instance.Map<DtoGroupEvent>(groupEvent);
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
                _context.Entry(groupEvent).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                result.Result = true;
                result.Entity = APAutoMapper.Instance.Map<DtoGroupEvent>(groupEvent);
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
