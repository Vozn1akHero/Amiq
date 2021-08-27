using Amiq.Contracts.Group;
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
        private AmiqContext _AmiqContext;

        public DaGroupParticipant()
        {
            _AmiqContext = new AmiqContext();
        }


        /// <summary>
        /// Zwraca listę grup w których bierze udział użytkownik
        /// </summary>
        public async Task<List<DtoGroup>> GetUserGroupsByUserIdAsync(int userId)
        {
            IQueryable dbGroups = (from g in _AmiqContext.Groups.AsNoTracking()
                                   join gp in _AmiqContext.GroupParticipants.AsNoTracking()
                                   on g.GroupId equals gp.GroupId
                                   join u in _AmiqContext.Users.AsNoTracking()
                                   on gp.UserId equals u.UserId
                                   where u.UserId == userId
                                   //select new DtoGroup { }
                                   select g
                                );
            List<DtoGroup> groups = await APAutoMapper.Instance.ProjectTo<DtoGroup>(dbGroups).ToListAsync();
            return groups;
        }

        public async Task LeaveGroupAsync(DtoLeaveGroup rsLeaveGroup)
        {
            var participant = _AmiqContext
                .GroupParticipants
                .SingleOrDefault(e=>e.UserId==rsLeaveGroup.UserId && e.GroupId == rsLeaveGroup.GroupId);
            if (participant != null)
            {
                _AmiqContext.GroupParticipants.Remove(participant);
                await _AmiqContext.SaveChangesAsync();
            }
        }

        public async Task JoinGroupAsync(DtoJoinGroup rsJoinGroup)
        {
            var participant = new GroupParticipant { 
                GroupId = rsJoinGroup.GroupId,
                UserId = rsJoinGroup.UserId
            };
            await _AmiqContext
                .GroupParticipants.AddAsync(participant);
            await _AmiqContext.SaveChangesAsync();
        }

        public async Task<DtoGroupParticipant> GetGroupParticipantAsync(DtoMinifiedGroupParticipant rsSimplifiedGroupParticipant)
        {
            var res = await _AmiqContext
                .GroupParticipants
                .Where(e => e.GroupId == rsSimplifiedGroupParticipant.GroupId
                && e.UserId == rsSimplifiedGroupParticipant.UserId)
                .Include(e=>e.User)
                .ProjectTo<DtoGroupParticipant>(APAutoMapper.Instance.ConfigurationProvider)
                .SingleOrDefaultAsync();

            return res;
        }

        /*private IConfigurationProvider MapperConfiguration()
        {
            var configuration = new MapperConfiguration(cfg =>
                        cfg.CreateMap<GroupParticipant, DtoGroupParticipant>()
                        );

            return configuration;
        }*/
    }
}
