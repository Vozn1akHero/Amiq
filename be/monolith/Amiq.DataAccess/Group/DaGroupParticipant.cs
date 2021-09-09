using Amiq.Contracts.Group;
using Amiq.Contracts.Utils;
using Amiq.DataAccess.Models;
using Amiq.DataAccess.Models.Models;
using Amiq.Mapping;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccess.Group
{
    public class DaGroupParticipant
    {
        private AmiqContext _amiqContext = new AmiqContext();
        private AmiqContextWithDebugLogging _amiqContextWithDebug = new AmiqContextWithDebugLogging();

        /// <summary>
        /// Zwraca listę grup w których bierze udział użytkownik
        /// </summary>
        public async Task<List<DtoGroup>> GetUserGroupsByUserIdAsync(int userId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            IQueryable dbGroups = (from g in _amiqContextWithDebug.Groups.AsNoTracking()
                                   join gp in _amiqContextWithDebug.GroupParticipants.AsNoTracking()
                                   on g.GroupId equals gp.GroupId
                                   join u in _amiqContextWithDebug.Users.AsNoTracking()
                                   on gp.UserId equals u.UserId
                                   where u.UserId == userId
                                   select g);
            List<DtoGroup> groups = await APAutoMapper.Instance.ProjectTo<DtoGroup>(dbGroups).ToListAsync();
            return groups;
        }

        public async Task LeaveGroupAsync(DtoLeaveGroup rsLeaveGroup)
        {
            var participant = _amiqContext
                .GroupParticipants
                .SingleOrDefault(e=>e.UserId==rsLeaveGroup.UserId && e.GroupId == rsLeaveGroup.GroupId);
            if (participant != null)
            {
                _amiqContext.GroupParticipants.Remove(participant);
                await _amiqContext.SaveChangesAsync();
            }
        }

        public async Task JoinGroupAsync(DtoJoinGroup rsJoinGroup)
        {
            var participant = new GroupParticipant { 
                GroupId = rsJoinGroup.GroupId,
                UserId = rsJoinGroup.UserId
            };
            await _amiqContext
                .GroupParticipants.AddAsync(participant);
            await _amiqContext.SaveChangesAsync();
        }

        public async Task<DtoGroupParticipant> GetGroupParticipantAsync(DtoMinifiedGroupParticipant rsSimplifiedGroupParticipant)
        {
            var res = await _amiqContext
                .GroupParticipants
                .Where(e => e.GroupId == rsSimplifiedGroupParticipant.GroupId
                && e.UserId == rsSimplifiedGroupParticipant.UserId)
                .Include(e=>e.User)
                .ProjectTo<DtoGroupParticipant>(APAutoMapper.Instance.ConfigurationProvider)
                .SingleOrDefaultAsync();

            return res;
        }
    }
}
