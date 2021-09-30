using Amiq.Contracts.Post;
using Amiq.Contracts.Utils;
using Amiq.DataAccess.Models.Models;
using Amiq.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccess.Post
{
    public class DaPostComment
    {
        private AmiqContext _amiqContext = new AmiqContext();

        public async Task<IEnumerable<DtoPostComment>> GetPostCommentAsync(Guid postId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            var result = new List<DtoPostComment>();

            var comments = _amiqContext
                .Comments
                .Where(e => e.PostId == postId && e.ParentId == null);
                //.Select(e=>new );

            //todo: procedura
            //throw new NotImplementedException();

            var commentsDto = await APAutoMapper.Instance.ProjectTo<DtoPostComment>(comments).ToListAsync();

            return commentsDto;
        }

        public Comment GetEntityById(Guid postCommentId) {
            return _amiqContext.Comments.SingleOrDefault(e => e.CommentId == postCommentId);
        }

        public async Task<DtoPostComment> CreateAsync(DtoNewPostComment newPostComment)
        {
            var entity = new Comment { 
                AuthorId = newPostComment.AuthorId,
                PostId = newPostComment.PostId,
                TextContent = newPostComment.Text
            };
            await _amiqContext.AddAsync(entity);
            await _amiqContext.SaveChangesAsync();
            DtoPostComment res = APAutoMapper.Instance.Map<DtoPostComment>(entity);
            return res;
        }

        public async Task<DtoPostComment> DeleteAsync(Guid postCommentId)
        {
            var entity = GetEntityById(postCommentId);
            if (entity != null)
            {
                _amiqContext.Comments.Remove(entity);
                await _amiqContext.SaveChangesAsync();
            }
            return APAutoMapper.Instance.Map<DtoPostComment>(entity);
        }

        public async Task<DtoPostComment> EditAsync(Guid postCommentId, string text)
        {
            var entity = GetEntityById(postCommentId);
            if (entity != null)
            {
                entity.TextContent = text;
                _amiqContext.Update(entity);
                await _amiqContext.SaveChangesAsync();
            }
            return APAutoMapper.Instance.Map<DtoPostComment>(entity);
        }
    }
}
