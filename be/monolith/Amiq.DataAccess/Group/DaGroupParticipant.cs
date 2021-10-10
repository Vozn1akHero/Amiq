﻿using Amiq.Contracts.Group;
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
        private DaGroupViewer _daGroupViewer = new DaGroupViewer();

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

        public async Task LeaveGroupAsync(int userId, int groupId)
        {
            var participant = _amiqContext
                .GroupParticipants
                .SingleOrDefault(e=>e.UserId == userId && e.GroupId == groupId);
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

        public async Task<DtoGroupViewer> GetGroupViewerByUserIdAsync(int userId, int groupId)
        {
            return new DtoGroupViewer
            {
                UserId = userId,
                GroupId = groupId,
                GroupViewerRole = await _daGroupViewer.GetRoleAsync(userId, groupId)
            };
        }

        public async Task<IEnumerable<DtoGroupParticipant>> GetGroupParticipantsAsync(int groupId, DtoPaginatedRequest paginatedRequest)
        {
            IQueryable query = (from gp in _amiqContextWithDebug.GroupParticipants.AsNoTracking()
                                   join g in _amiqContextWithDebug.Groups.AsNoTracking()
                                   on gp.GroupId equals g.GroupId
                                   join u in _amiqContextWithDebug.Users.AsNoTracking() on gp.UserId equals u.UserId
                                   where g.GroupId == groupId
                                   select gp)
                                   .Skip((paginatedRequest.Page-1)*paginatedRequest.Count)
                                   .Take(paginatedRequest.Count);
            var participants = await APAutoMapper.Instance.ProjectTo<DtoGroupParticipant>(query).ToListAsync();
            return participants;
        }
    }

    // TODO CACHE
    class DaGroupViewer
    {
        private AmiqContext _amiqContext = new AmiqContext();

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

        bool IsParticipant(int userId, int groupId) => _amiqContext.GroupParticipants.AsNoTracking()
            .Any(e=>e.GroupId==groupId && e.UserId == userId && !e.IsAdmin);

        bool IsCreator(int userId, int groupId) => _amiqContext.Groups.AsNoTracking().Any(e => e.GroupId == groupId && e.CreatedBy == userId);

        bool IsAdmin(int userId, int groupId) => _amiqContext.GroupParticipants.AsNoTracking().Any(e => e.UserId == userId && 
            e.IsAdmin && e.GroupId == groupId);

        bool IsGuest(int userId, int groupId) => !_amiqContext.GroupParticipants.AsNoTracking().Any(e => e.GroupId == groupId 
            && e.UserId == userId);

        bool IsBanned(int userId, int groupId) => _amiqContext.GroupBlockedUsers.AsNoTracking()
            .Any(e=>e.GroupId == groupId && e.UserId==userId);
    }
}