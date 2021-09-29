using Amiq.Contracts.Group;
using Amiq.DataAccess.Models.Models;

namespace Amiq.Mapping.Group
{
    public class GroupParticipantProfile : APProfile
    {
        public GroupParticipantProfile()
        {
            CreateMap<GroupParticipant, DtoGroupParticipant>()
                .ForMember(e=>e.UserId, g=>g.MapFrom(h=>h.User.UserId))
                .ForMember(e=>e.Name, g=>g.MapFrom(h=>h.User.Name))
                .ForMember(e=>e.Surname, g=>g.MapFrom(h=>h.User.Surname))
                .ForMember(e=>e.AvatarPath, g=>g.MapFrom(h=>h.User.AvatarPath));
        }
    }
}
