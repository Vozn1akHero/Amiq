using AmicaPlus.Contracts.Group;
using AmicaPlus.Contracts.User;
using AmicaPlus.DataAccess.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<DtoGroupParticipant>> GetGroupParticipantsAsync(int groupId)
        {
            return await _amicaPlusContext.GroupParticipants
                .Where(e => e.GroupId == groupId)
                .Join(_amicaPlusContext.Users,
                participant => participant.UserId,
                user => user.UserId, (participant, user) => new DtoGroupParticipant { 
                    UserInfo = new DtoUserInfo { },
                    GroupId = participant.GroupId
                })
                .ToListAsync();
        }
    }
}
