using Amiq.Services.Group.Contracts;
using Amiq.Services.Group.Contracts.Utils;
using Amiq.Services.Group.DataAccessLayer.Models.Models;
using Amiq.Services.Group.Mapping;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.Group.DataAccessLayer
{
    public class DaoGroupParticipant
    {
        private AmiqGroupContext _amiqGroupContext = new AmiqGroupContext();
        private DaGroupViewer _daGroupViewer = new DaGroupViewer();

        public async Task<DtoGroupUserParams> GetGroupUserParamsAsync(int userId, int groupId)
        {
            DtoGroupUserParams result = new();
            result.GroupId = groupId;
            result.IsHidden = await _amiqGroupContext.HiddenGroups.AnyAsync(e => e.UserId == userId && e.GroupId == groupId);
            return result;
        }

        /// <summary>
        /// Zwraca listę grup w których bierze udział użytkownik
        /// </summary>
        public async Task<DtoListResponseOf<DtoGroupCard>> GetUserGroupsByUserIdAsync(int userId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            DtoListResponseOf<DtoGroupCard> result = new();
            result.Entities = await _amiqGroupContext.Groups.AsNoTracking()
                .Where(e => e.GroupParticipants.Any(gp => gp.UserId == userId))
                .Select(g => new DtoGroupCard
                {
                    GroupId = g.GroupId,
                    Name = g.Name,
                    AvatarSrc = g.AvatarSrc,
                    Description = g.Description,
                    ParticipantsCount = g.GroupParticipants.Count,
                    IsHidden = g.HiddenGroups.Any(hg => hg.UserId == userId && hg.GroupId == g.GroupId),
                    IsRequestCreatorParticipant = true
                })
                .Skip((dtoPaginatedRequest.Page - 1) * dtoPaginatedRequest.Count)
                .Take(dtoPaginatedRequest.Count)
                .ToListAsync();
            result.Length = await _amiqGroupContext.Groups.AsNoTracking()
                .Where(e => e.GroupParticipants.Any(gp => gp.UserId == userId))
                .CountAsync();
            return result;
        }

        public async Task DeleteParticipantAsync(GroupParticipant groupParticipant)
        {
            _amiqGroupContext.Remove(groupParticipant);
            await _amiqGroupContext.SaveChangesAsync();
        }

        public async Task<DtoListResponseOf<DtoGroupCard>> GetAdministeredUserGroupsByUserIdAsync(int userId,
            DtoPaginatedRequest dtoPaginatedRequest)
        {
            DtoListResponseOf<DtoGroupCard> result = new();
            var query = (from g in _amiqGroupContext.Groups.AsNoTracking()
                         join gp in _amiqGroupContext.GroupParticipants.AsNoTracking()
                         on g.GroupId equals gp.GroupId
                         join u in _amiqGroupContext.Users.AsNoTracking()
                         on gp.UserId equals u.UserId
                         where u.UserId == userId && gp.IsAdmin
                         select new DtoGroupCard
                         {
                             GroupId = g.GroupId,
                             Name = g.Name,
                             AvatarSrc = g.AvatarSrc,
                             Description = g.Description,
                             ParticipantsCount = g.GroupParticipants.Count,
                             IsHidden = g.HiddenGroups.Any(hg => hg.UserId == userId && hg.GroupId == g.GroupId),
                             IsRequestCreatorParticipant = gp.UserId == userId,
                         });
            result.Entities = await query
                                   .Skip((dtoPaginatedRequest.Page - 1) * dtoPaginatedRequest.Count)
                                   .Take(dtoPaginatedRequest.Count)
                                   .ToListAsync();
            result.Length = await query.CountAsync();
            return result;
        }

        public async Task<DtoListResponseOf<DtoGroupCard>> GetNonAdministeredUserGroupsByUserIdAsync(int userId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            DtoListResponseOf<DtoGroupCard> result = new();
            var query = (from g in _amiqGroupContext.Groups.AsNoTracking()
                         join gp in _amiqGroupContext.GroupParticipants.AsNoTracking()
                         on g.GroupId equals gp.GroupId
                         join u in _amiqGroupContext.Users.AsNoTracking()
                         on gp.UserId equals u.UserId
                         where u.UserId == userId && !gp.IsAdmin
                         select new DtoGroupCard
                         {
                             GroupId = g.GroupId,
                             Name = g.Name,
                             AvatarSrc = g.AvatarSrc,
                             Description = g.Description,
                             ParticipantsCount = g.GroupParticipants.Count,
                             IsHidden = g.HiddenGroups.Any(hg => hg.UserId == userId && hg.GroupId == g.GroupId),
                             IsRequestCreatorParticipant = gp.UserId == userId,
                         });
            result.Entities = await query
                            .Skip((dtoPaginatedRequest.Page - 1) * dtoPaginatedRequest.Count)
                            .Take(dtoPaginatedRequest.Count)
                            .ToListAsync();
            result.Length = await query.CountAsync();
            return result;
        }

        public async Task<DtoListResponseOf<DtoGroupCard>> GetHiddenUserGroupsByUserIdAsync(int userId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            DtoListResponseOf<DtoGroupCard> result = new();
            var query = _amiqGroupContext.HiddenGroups.AsNoTracking()
                                   .Where(e => e.UserId == userId);
            result.Entities = await query
                                   .Select(g => new DtoGroupCard
                                   {
                                       GroupId = g.GroupId,
                                       Name = g.Group.Name,
                                       AvatarSrc = g.Group.AvatarSrc,
                                       Description = g.Group.Description,
                                       ParticipantsCount = g.Group.GroupParticipants.Count,
                                       IsHidden = true,
                                       IsRequestCreatorParticipant = g.Group.GroupParticipants.Any(gp => gp.UserId == userId)
                                   })
                                   .Skip((dtoPaginatedRequest.Page - 1) * dtoPaginatedRequest.Count)
                                   .Take(dtoPaginatedRequest.Count)
                                   .ToListAsync();
            result.Length = await query.CountAsync();
            return result;
        }

        public async Task LeaveGroupAsync(int userId, int groupId)
        {
            var participant = _amiqGroupContext
                .GroupParticipants
                .SingleOrDefault(e => e.UserId == userId && e.GroupId == groupId);
            if (participant != null)
            {
                _amiqGroupContext.GroupParticipants.Remove(participant);
                await _amiqGroupContext.SaveChangesAsync();
            }
        }

        public async Task JoinGroupAsync(int userId, int groupId)
        {
            var participant = new GroupParticipant
            {
                GroupId = groupId,
                UserId = userId
            };
            await _amiqGroupContext
                .GroupParticipants.AddAsync(participant);
            await _amiqGroupContext.SaveChangesAsync();
        }

        public GroupParticipant GetGroupParticipant(int userId, int groupId)
        {
            return _amiqGroupContext.GroupParticipants.SingleOrDefault(e => e.UserId == userId && e.GroupId == groupId);
        }

        public async Task<DtoGroupParticipant> GetGroupParticipantAsync(DtoMinifiedGroupParticipant rsSimplifiedGroupParticipant)
        {
            var res = await _amiqGroupContext
                .GroupParticipants
                .AsNoTracking()
                .Where(e => e.GroupId == rsSimplifiedGroupParticipant.GroupId
                && e.UserId == rsSimplifiedGroupParticipant.UserId)
                .Include(e => e.User)
                .ProjectTo<DtoGroupParticipant>(AmiqGroupAutoMapper.Instance.ConfigurationProvider)
                .SingleOrDefaultAsync();

            return res;
        }

        public async Task<DtoGroupViewer> GetGroupViewerByUserIdAsync(int userId, int groupId)
        {
            return new DtoGroupViewer
            {
                UserId = userId,
                GroupId = groupId,
                GroupViewerRole = await _daGroupViewer.GetRoleAsync(userId, groupId)
            };
        }

        public async Task<DtoListResponseOf<DtoGroupParticipant>> GetGroupParticipantsAsync(int groupId, DtoPaginatedRequest paginatedRequest)
        {
            DtoListResponseOf<DtoGroupParticipant> result = new();
            IQueryable query = (from gp in _amiqGroupContext.GroupParticipants.AsNoTracking()
                                join g in _amiqGroupContext.Groups.AsNoTracking()
                                on gp.GroupId equals g.GroupId
                                join u in _amiqGroupContext.Users.AsNoTracking() on gp.UserId equals u.UserId
                                where g.GroupId == groupId
                                select gp)
                                   .Skip((paginatedRequest.Page - 1) * paginatedRequest.Count)
                                   .Take(paginatedRequest.Count);
            result.Length = await _amiqGroupContext.GroupParticipants.AsNoTracking().Where(e => e.GroupId == groupId).CountAsync();
            result.Entities = await AmiqGroupAutoMapper.Instance.ProjectTo<DtoGroupParticipant>(query).ToListAsync();
            return result;
        }
    }

    internal class DaGroupViewer
    {
        private AmiqGroupContext _amiqGroupContext = new AmiqGroupContext();

        public async Task<EnGroupViewerRole> GetRoleAsync(int userId, int groupId)
        {
            return await Task.Run(() =>
            {
                if (IsBanned(userId, groupId))
                    return EnGroupViewerRole.Blocked;
                else if (IsGuest(userId, groupId))
                    return EnGroupViewerRole.Guest;
                else if (IsParticipant(userId, groupId))
                    return EnGroupViewerRole.Participant;
                else if (IsCreator(userId, groupId))
                    return EnGroupViewerRole.Creator;
                else if (IsAdmin(userId, groupId))
                    return EnGroupViewerRole.Admin;
                return EnGroupViewerRole.Guest;
            });
        }

        bool IsParticipant(int userId, int groupId) => _amiqGroupContext.GroupParticipants.AsNoTracking()
            .Any(e => e.GroupId == groupId && e.UserId == userId && !e.IsAdmin);

        bool IsCreator(int userId, int groupId) => _amiqGroupContext.Groups.AsNoTracking().Any(e => e.GroupId == groupId && e.CreatedBy == userId);

        bool IsAdmin(int userId, int groupId) => _amiqGroupContext.GroupParticipants.AsNoTracking().Any(e => e.UserId == userId &&
            e.IsAdmin && e.GroupId == groupId);

        bool IsGuest(int userId, int groupId) => !_amiqGroupContext.GroupParticipants.AsNoTracking().Any(e => e.GroupId == groupId
            && e.UserId == userId);

        bool IsBanned(int userId, int groupId) => _amiqGroupContext.GroupBlockedUsers.AsNoTracking()
            .Any(e => e.GroupId == groupId && e.UserId == userId);
    }
}
