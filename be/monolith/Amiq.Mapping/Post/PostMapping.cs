using Amiq.Contracts.Post;
using Amiq.DataAccessLayer.Models.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Amiq.Mapping.Post
{
    public class PostMapping : APProfile
    {
        public PostMapping()
        {
            CreateMap<UserPost, DtoUserPost>()
                .ForMember(e => e.Author, dest => dest.MapFrom(i => i.User))
                //.ForMember(e => e.Name, dest => dest.MapFrom(i => i.User.Name))
                //.ForMember(e => e.Surname, dest => dest.MapFrom(i => i.User.Surname))
                .ForMember(e => e.AvatarPath, dest => dest.MapFrom(i => i.User.AvatarPath))
                .ForMember(e => e.TextContent, dest => dest.MapFrom(i => i.Post.TextContent))
                .ForMember(e => e.CreatedAt, dest => dest.MapFrom(i => i.Post.CreatedAt))
                .ForMember(e => e.EditedAt, dest => dest.MapFrom(i => i.Post.EditedAt))
                .ForMember(e => e.EditedBy, dest => dest.MapFrom(i => i.Post.EditedBy))
                .ForMember(e => e.CommentsCount, dest => dest.MapFrom(i => i.Post.Comments.Where(e => !e.ParentId.HasValue && !e.IsRemoved).Count()));
                //.ForMember(e => e.HasMoreCommentsThanRecent, dest => dest.MapFrom(i=> i.Post.Comments != null && i.Post.Comments.Where(e=>!e.ParentId.HasValue).Count() > 5))
                //.ForMember(e => e.RecentComments, dest => dest.MapFrom(i => i.Post.Comments.Where(e=>!e.ParentId.HasValue).OrderByDescending(e=>e.CreatedAt).Take(5)));

            CreateMap<GroupPost, DtoGroupPost>()
                .ForMember(e => e.Author, dest => dest.MapFrom(i => i.Author))
                .ForMember(e => e.GroupName, dest => dest.MapFrom(i => i.Group.Name))
                .ForMember(e => e.AvatarPath, dest => dest.MapFrom(i => i.Group.AvatarSrc))
                .ForMember(e => e.TextContent, dest => dest.MapFrom(i => i.Post.TextContent))
                .ForMember(e => e.CreatedAt, dest => dest.MapFrom(i => i.Post.CreatedAt))
                .ForMember(e => e.EditedAt, dest => dest.MapFrom(i => i.Post.EditedAt))
                .ForMember(e => e.EditedBy, dest => dest.MapFrom(i => i.Post.EditedBy))
                .ForMember(e => e.CommentsCount, dest => dest.MapFrom(i => i.Post.Comments.Where(e => !e.ParentId.HasValue && !e.IsRemoved).Count()))
                //.ForMember(e => e.HasMoreCommentsThanRecent, dest => dest.MapFrom(i => i.Post.Comments.Where(e => !e.ParentId.HasValue).Count() > 5))
                //.ForMember(e => e.RecentComments, dest => dest.MapFrom(i => i.Post.Comments.Where(e => !e.ParentId.HasValue).OrderByDescending(e => e.CreatedAt).Take(5)))
                ;

            CreateTwoWayMap<DtoPostCreation, UserPost>();
            CreateTwoWayMap<DtoPostCreation, GroupPost>();
            CreateTwoWayMap<DtoPostCreation, DataAccessLayer.Models.Models.Post>();
        }
    }
}
