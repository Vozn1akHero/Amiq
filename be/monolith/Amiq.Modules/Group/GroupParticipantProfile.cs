using Amiq.Contracts.Group;
using Amiq.DataAccess.Models.Models;

namespace Amiq.Mapping.Group
{
    public class GroupParticipantProfile : APProfile
    {
        public GroupParticipantProfile()
        {
            CreateMap<GroupParticipant, DtoGroupParticipant>();
        }
    }
}
