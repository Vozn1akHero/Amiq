using Amiq.Contracts.Post;
using Amiq.DataAccess.Models.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Mapping.Post
{
    public class PostCommentMapping : APProfile
    {
        public PostCommentMapping()
        {
            CreateMap<Comment, DtoPostComment>().ForMember(dest => dest.IsChild, opt => opt.MapFrom<IsChildResolver>());
        }
    }

    internal class IsChildResolver : IValueResolver<Comment, DtoPostComment, bool>
    {
        public bool Resolve(Comment source, DtoPostComment destination, bool destMember, ResolutionContext context)
        {
            return source.ParentId != null;
        }
    }
}
