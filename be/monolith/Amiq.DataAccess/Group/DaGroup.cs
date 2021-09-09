using Amiq.Contracts.Group;
using Amiq.Contracts.User;
using Amiq.DataAccess.Models.Models;
using Amiq.Mapping;
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
            IQueryable query = _AmiqContext.GroupParticipants
                .Where(e => e.GroupId == groupId)
                .Join(_AmiqContext.Users,
                    participant => participant.UserId,
                    user => user.UserId,
                    (participant, user) => new { Participant = participant, User = user });
            var data = await APAutoMapper.Instance.ProjectTo<DtoGroupParticipant>(query).ToListAsync();
            return data;
        }
    }
}
