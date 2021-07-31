using AmicaPlus.DataAccess.Models;
using AmicaPlus.DataAccess.Models.Models;
using AmicaPlus.Modules.Mapping;
using AmicaPlus.ResultSets.Auth;
using AmicaPlus.ResultSets.Group;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AmicaPlus.DataAccess.Group
{
    public class DaGroup
    {
        private AmicaPlusContext _amicaPlusContext;

        public DaGroup()
        {
            _amicaPlusContext = new AmicaPlusContext();
        }

        /// <summary>
        /// Zwraca listę grup w których bierze udział użytkownik
        /// </summary>
        public async Task<List<RsGroup>> GetUserGroupsByUserIdAsync(int userId)
        {
            IQueryable dbGroups =  (from g in _amicaPlusContext.Groups.AsNoTracking()
                                join gp in _amicaPlusContext.GroupParticipants.AsNoTracking()
                                on g.GroupId equals gp.GroupId
                                join u in _amicaPlusContext.Users.AsNoTracking()
                                on gp.UserId equals u.UserId
                                where u.UserId == userId
                                    //select new RsGroup { }
                                select g
                                );
            List<RsGroup> groups = await APAutoMapper.Instance.ProjectTo<RsGroup>(dbGroups).ToListAsync();
            return groups;
        }

        public async Task<List<RsGroupParticipant>> GetGroupParticipantsAsync(int groupId)
        {
            return await _amicaPlusContext.GroupParticipants
                .Where(e => e.GroupId == groupId)
                .Join(_amicaPlusContext.Users,
                participant => participant.UserId,
                user => user.UserId, (participant, user) => new RsGroupParticipant { 
                    UserInfo = new RsUserInfo { },
                    GroupId = participant.GroupId
                })
                .ToListAsync();
        }
    }
}
