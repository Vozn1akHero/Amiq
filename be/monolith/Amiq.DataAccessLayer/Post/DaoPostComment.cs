using Amiq.Common;
using Amiq.Common.Enums;
using Amiq.Contracts;
using Amiq.Contracts.Post;
using Amiq.Contracts.Utils;
using Amiq.DataAccessLayer.Models.Models;
using Amiq.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccessLayer.Post
{
    public class DaoPostComment
    {
        private AmiqContext _amiqContext = new AmiqContext();

        public async Task<DtoListResponseOf<DtoPostComment>> GetUserPostCommentAsync(Guid postId, DtoPaginatedRequest paginatedRequest)
        {
            var result = new DtoListResponseOf<DtoPostComment>();

            var commentsQuery = _amiqContext
                    .Comments
                    .Where(e => e.PostId == postId && !e.ParentId.HasValue && !e.IsRemoved)
                    .Paginate(paginatedRequest.Page, paginatedRequest.Count)
                    .OrderByDescending(e => e.CreatedAt);
            result.Entities = await APAutoMapper.Instance.ProjectTo<DtoPostComment>(commentsQuery).ToListAsync();
            result.Length = await _amiqContext
                    .Comments
                    .Where(e => e.PostId == postId && !e.ParentId.HasValue && !e.IsRemoved)
                    .CountAsync();

            return result;
        }

        public async Task<DtoListResponseOf<DtoGroupPostComment>> GetGroupPostCommentsAsync(Guid postId, DtoPaginatedRequest paginatedRequest)
        {
            DtoListResponseOf<DtoGroupPostComment> result = new();

            IQueryable commentsQuery = _amiqContext
                    .GroupPostComments
                    .Where(e => e.Comment.PostId == postId && !e.Comment.ParentId.HasValue && !e.Comment.IsRemoved)
                    .Paginate(paginatedRequest.Page, paginatedRequest.Count)
                    .OrderByDescending(e => e.Comment.CreatedAt);
            result.Entities = await APAutoMapper.Instance.ProjectTo<DtoGroupPostComment>(commentsQuery).ToListAsync();
            result.Length = await _amiqContext
                    .GroupPostComments
                    .Where(e => e.Comment.PostId == postId && !e.Comment.ParentId.HasValue && !e.Comment.IsRemoved)
                    .CountAsync();

            return result;
        }

        public Comment GetEntityById(Guid postCommentId) {
            return _amiqContext.Comments.SingleOrDefault(e => e.CommentId == postCommentId);
        }

        public async Task<DtoPostComment> CreateAsync(int authorId, DtoCreatePostComment newPostComment)
        {
            var entity = new Comment { 
                AuthorId = authorId,
                PostId = newPostComment.PostId,
                TextContent = newPostComment.TextContent,
                //AuthorVisibilityType = newPostComment.AuthorVisibilityType,
                ParentId = newPostComment.ParentId,
                MainParentId = newPostComment.MainParentId,
                //GroupId = newPostComment.GroupId.HasValue ? newPostComment.GroupId.Value : null
            };
            await _amiqContext.AddAsync(entity);
            await _amiqContext.SaveChangesAsync();
            IQueryable createdCommentQuery = _amiqContext.Comments.Where(e=>e.CommentId == entity.CommentId);
            DtoPostComment res = APAutoMapper.Instance.ProjectTo<DtoPostComment>(createdCommentQuery).Single();
            return res;
        }

        public async Task<DtoGroupPostComment> CreateGroupPostCommentAsync(int authorId, DtoCreateGroupPostComment dtoCreateGroupPostComment)
        {
            var entity = new GroupPostComment
            {
                Comment = new Comment
                {
                    AuthorId = authorId,
                    PostId = dtoCreateGroupPostComment.PostId,
                    TextContent = dtoCreateGroupPostComment.TextContent,
                    ParentId = dtoCreateGroupPostComment.ParentId,
                    MainParentId = dtoCreateGroupPostComment.MainParentId,
                },
                AuthorVisibilityType = dtoCreateGroupPostComment.AuthorVisibilityType,
                GroupId = dtoCreateGroupPostComment.GroupId,
                MainParentId = dtoCreateGroupPostComment.GroupCommentMainParentId,
                ParentId = dtoCreateGroupPostComment.GroupCommentParentId
            };
            _amiqContext.GroupPostComments.Add(entity);
            await _amiqContext.SaveChangesAsync();
            IQueryable createdCommentQuery = _amiqContext.GroupPostComments.Where(e => e.CommentId == entity.CommentId);
            var groupPostComment = APAutoMapper.Instance.ProjectTo<DtoGroupPostComment>(createdCommentQuery).Single();
            return groupPostComment;
        }

        public async Task<DtoPostComment> DeleteAsync(Guid postCommentId)
        {
            var entity = GetEntityById(postCommentId);
            if (entity != null)
            {
                entity.IsRemoved = true;
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
