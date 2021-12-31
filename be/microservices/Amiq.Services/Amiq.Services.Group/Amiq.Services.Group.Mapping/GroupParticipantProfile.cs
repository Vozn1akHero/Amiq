using Amiq.Services.Group.Contracts;
using Amiq.Services.Group.DataAccessLayer.Models.Models;
using AutoMapper;

namespace Amiq.Services.Group.Mapping
{
    public class GroupParticipantProfile : Profile
    {
        public GroupParticipantProfile()
        {
            CreateMap<GroupParticipant, DtoGroupParticipant>()
                .ForMember(e => e.UserId, g => g.MapFrom(h => h.User.UserId))
                .ForMember(e => e.Name, g => g.MapFrom(h => h.User.Name))
                .ForMember(e => e.Surname, g => g.MapFrom(h => h.User.Surname))
                .ForMember(e => e.AvatarPath, g => g.MapFrom(h => h.User.AvatarPath));
        }
    }
}
