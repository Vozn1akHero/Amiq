using AmicaPlus.DataAccess.Models;
using AmicaPlus.DataAccess.Models.Models;
using AmicaPlus.Mapping;
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
