using Amiq.Contracts.Group;
using Amiq.Contracts.User;
using Amiq.DataAccess.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amiq.DataAccess.Group
{
    public class DaGroup
    {
        private AmiqContext _AmiqContext;

        public DaGroup()
        {
            _AmiqContext = new AmiqContext();
        }

        public async Task<List<DtoGroupParticipant>> GetGroupParticipantsAsync(int groupId)
        {
            return await _AmiqContext.GroupParticipants
                .Where(e => e.GroupId == groupId)
                .Join(_AmiqContext.Users,
                participant => participant.UserId,
                user => user.UserId, (participant, user) => new DtoGroupParticipant { 
                    UserInfo = new DtoUserInfo { },
                    GroupId = participant.GroupId
                })
                .ToListAsync();
        }
    }
}
