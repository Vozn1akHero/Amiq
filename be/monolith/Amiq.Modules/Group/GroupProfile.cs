using Amiq.Contracts.Core;
using Amiq.Contracts.Group;
using Amiq.DataAccess.Models.Models;
using AutoMapper;

namespace Amiq.Mapping
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<GroupDescriptionBlock, DtoDescriptionBlock>()
                .ForMember(e => e.TextBlockId, d => d.MapFrom(g => g.TextBlock.TextBlockId))
                .ForMember(e => e.Header, d => d.MapFrom(g => g.TextBlock.Header))
                .ForMember(e => e.Content, d => d.MapFrom(g => g.TextBlock.Content));

            CreateMap<DataAccess.Models.Models.Group, DtoGroup>()
                .ForMember(e=>e.DescriptionBlocks, d=>d.MapFrom(g=>g.GroupDescriptionBlocks))
                //.ForMember(e=>e.Participants, d => d.MapFrom(g=>g.GroupParticipants));
                .ForMember(e=>e.ParticipantsCount, d => d.MapFrom(g=>g.GroupParticipants.Count));
        }
    }
}
