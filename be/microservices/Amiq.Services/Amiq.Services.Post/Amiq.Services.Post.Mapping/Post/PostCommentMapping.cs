using Amiq.Contracts.Post;
using Amiq.DataAccessLayer.Models.Models;

namespace Amiq.Services.Post.Mapping.Post
{
    public class PostCommentMapping : APProfile
    {
        public PostCommentMapping()
        {
            CreateMap<Comment, DtoPostComment>()
                .PreserveReferences()
                .ForMember(e => e.Author, dest => dest.MapFrom(i => i.Author))
                .ForMember(e => e.Children, dest => dest.MapFrom(i => i.InverseMainParent))
                //.ForMember(e => e.Parent, dest => dest.MapFrom(i => i.Parent))
                //.ForMember(e => e.MainParent, dest => dest.MapFrom(i => i.MainParent))
                .ForMember(e => e.ParentCommentId, dest => dest.MapFrom(i => i.ParentId))
                .ForMember(e => e.ParentCommentAuthor, dest => dest.MapFrom(i => i.Parent.Author))
                .ForMember(e => e.MainParentCommentId, dest => dest.MapFrom(i => i.MainParentId));

            CreateMap<Comment, DtoGroupPostComment>().PreserveReferences();

            CreateMap<GroupPostComment, DtoGroupPostComment>()
                .PreserveReferences()
                .ForMember(e => e.PostId, dest => dest.MapFrom(i => i.Comment.PostId))
                .ForMember(e => e.TextContent, dest => dest.MapFrom(i => i.Comment.TextContent))
                .ForMember(e => e.CreatedAt, dest => dest.MapFrom(i => i.Comment.CreatedAt))
                .ForMember(e => e.Group, dest => dest.MapFrom(i => i.Group))
                .ForMember(e => e.Author, dest => dest.MapFrom(i => i.Comment.Author))
                .ForMember(e => e.Children, dest => dest.MapFrom(i => i.InverseMainParent))
                .ForMember(e => e.ParentCommentId, dest => dest.MapFrom(i => i.Comment.ParentId))
                .ForMember(e => e.ParentCommentAuthor, dest => dest.MapFrom(i => i.Comment.Parent.Author))
                .ForMember(e => e.MainParentCommentId, dest => dest.MapFrom(i => i.Comment.MainParentId))
                .ForMember(e => e.GroupCommentParentId, dest => dest.MapFrom(i => i.ParentId))
                .ForMember(e => e.GroupCommentMainParentId, dest => dest.MapFrom(i => i.MainParentId))
                .ForMember(e => e.IsRemoved, dest => dest.MapFrom(i => i.Comment.IsRemoved));
        }
    }
}
