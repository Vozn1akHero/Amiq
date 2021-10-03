using Amiq.Contracts.Core;
using Amiq.Contracts.Group;
using Amiq.Contracts.User;
using Amiq.DataAccess.Models.Models;
using Amiq.Mapping;
using Microsoft.Data.SqlClient.DataClassification;
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

        public async Task<IEnumerable<DtoGroup>> GetByNameAsync(string name)
        {
            var query = _AmiqContext.Groups.Where(e => e.Name.StartsWith(name));
            var data = await APAutoMapper.Instance.ProjectTo<DtoGroup>(query).ToListAsync();
            return data;
        }

        public async Task<DtoGroup> GetGroupById(int groupId)
        {
            var query = _AmiqContext.Groups.Where(e => e.GroupId == groupId);
            var data = await APAutoMapper.Instance.ProjectTo<DtoGroup>(query).SingleOrDefaultAsync();
            return data;
        }

        /*public async Task<DtoDescriptionBlock> GetGroupDescriptionByGroupId(int groupId)
        {

        }*/
    }
}
