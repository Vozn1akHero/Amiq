using Amiq.Contracts.Post;
using Amiq.DataAccess.Models.Models;

namespace Amiq.Mapping.Post
{
    public class PostMapping : APProfile
    {
        public PostMapping()
        {
            CreateMap<UserPost, DtoUserPost>()
                .ForMember(e=>e.TextContent, dest => dest.MapFrom(i => i.Post.TextContent))
                .ForMember(e => e.CreatedAt, dest => dest.MapFrom(i => i.Post.CreatedAt))
                .ForMember(e => e.EditedAt, dest => dest.MapFrom(i => i.Post.EditedAt))
                .ForMember(e => e.EditedBy, dest => dest.MapFrom(i => i.Post.EditedBy));

            CreateMap<GroupPost, DtoGroupPost>()
                .ForMember(e => e.TextContent, dest => dest.MapFrom(i => i.Post.TextContent))
                .ForMember(e => e.CreatedAt, dest => dest.MapFrom(i => i.Post.CreatedAt))
                .ForMember(e => e.EditedAt, dest => dest.MapFrom(i => i.Post.EditedAt))
                .ForMember(e => e.EditedBy, dest => dest.MapFrom(i => i.Post.EditedBy));
        }
    }
}
