using Amiq.Contracts.Post;
using Amiq.DataAccess.Models.Models;

namespace Amiq.Mapping.Post
{
    public class PostCommentMapping : APProfile
    {
        public PostCommentMapping()
        {
            CreateMap<Comment, DtoPostComment>()
                .PreserveReferences()
                .ForMember(e => e.Author, dest => dest.MapFrom(i => i.Author))
                .ForMember(e => e.Group, dest => dest.MapFrom(i => i.Group))
                .ForMember(e => e.Children, dest => dest.MapFrom(i => i.InverseMainParent))
                .ForMember(e => e.Parent, dest => dest.MapFrom(i => i.Parent))
                .ForMember(e => e.ParentCommentId, dest => dest.MapFrom(i => i.ParentId))
                //.ForMember(e=>e.HasMoreChildrenThanPassed, dest=> dest.MapFrom(i => i.InverseMainParent.))
                //.PreserveReferences()
                //.MaxDepth(300)
                //.ForMember(dest => dest.IsChild, opt => opt.MapFrom(i => i.ParentId.HasValue))
                /*.AfterMap((src, dest) => {
                    if (dest.IsChild)
                    {
                        dest.Parent = APAutoMapper.Instance.Map<DtoPostComment>(src).Parent;
                    }
                })*/;
        }
    }
}
