using Amiq.Services.Group.Contracts.Group;
using Amiq.Services.Group.DataAccessLayer.Models;
using AutoMapper;

namespace Amiq.Services.Group.Mapping
{
    public class GroupEventProfile : Profile
    {
        public GroupEventProfile()
        {
            CreateMap<GroupEventParticipant, DtoGroupEventParticipant>();
            CreateMap<GroupEvent, DtoGroupEvent>()
                .ForPath(e => e.GroupEventParticipantsCount, d => d.MapFrom(g => g.GroupEventParticipants.Count));
        }
    }
}
