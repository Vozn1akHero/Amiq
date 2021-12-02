using Amiq.Services.Group.Contracts;
using Amiq.Services.Group.DataAccessLayer.Models.Models;
using AutoMapper;

namespace Amiq.Services.Group.Mapping
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<GroupDescriptionBlock, DtoDescriptionBlock>()
                .ForMember(e => e.TextBlockId, d => d.MapFrom(g => g.TextBlock.TextBlockId))
                .ForMember(e => e.Header, d => d.MapFrom(g => g.TextBlock.Header))
                .ForMember(e => e.Content, d => d.MapFrom(g => g.TextBlock.Content));

            CreateMap<DataAccessLayer.Models.Models.Group, DtoGroup>()
                .ForMember(e => e.DescriptionBlocks, d => d.MapFrom(g => g.GroupDescriptionBlocks))
                .ForMember(e => e.ParticipantsCount, d => d.MapFrom(g => g.GroupParticipants.Count));

            CreateMap<DataAccessLayer.Models.Models.Group, DtoMinifiedGroup>();
        }
    }
}