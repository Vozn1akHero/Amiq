using Amiq.Services.Post.Common;
using Amiq.Services.Post.Contracts.Post;
using Amiq.Services.Post.Contracts.Utils;
using Amiq.Services.Post.DataAccessLayer.Models;
using Amiq.Services.Post.DataAccessLayer.Models.Models;
using Amiq.Services.Post.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Amiq.Services.Post.DataAccessLayer.Post
{
    public class DaoPostComment
    {
        private AmiqPostContext _amiqContext = new AmiqPostContext();

        public async Task<DtoListResponseOf<DtoPostComment>> GetUserPostCommentAsync(Guid postId, DtoPaginatedRequest paginatedRequest)
        {
            var result = new DtoListResponseOf<DtoPostComment>();

            var commentsQuery = _amiqContext
                    .Comments
                    .Where(e => e.PostId == postId && !e.ParentId.HasValue && !e.IsRemoved)
                    .Paginate(paginatedRequest.Page, paginatedRequest.Count)
                    .OrderByDescending(e => e.CreatedAt);
            result.Entities = await AmiqPostAutoMapper.Instance.ProjectTo<DtoPostComment>(commentsQuery).ToListAsync();
            result.Length = await _amiqContext
                    .Comments
                    .Where(e => e.PostId == postId && !e.IsRemoved)
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
            result.Entities = await AmiqPostAutoMapper.Instance.ProjectTo<DtoGroupPostComment>(commentsQuery).ToListAsync();
            result.Length = await _amiqContext
                    .Comments
                    .Where(e => e.PostId == postId && !e.IsRemoved)
                    .CountAsync();

            return result;
        }

        public Comment GetEntityById(Guid postCommentId)
        {
            return _amiqContext.Comments.SingleOrDefault(e => e.CommentId == postCommentId);
        }

        public async Task<DtoPostComment> CreateAsync(int authorId, DtoCreatePostComment newPostComment)
        {
            var entity = new Comment
            {
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
            IQueryable createdCommentQuery = _amiqContext.Comments.Where(e => e.CommentId == entity.CommentId);
            DtoPostComment res = AmiqPostAutoMapper.Instance.ProjectTo<DtoPostComment>(createdCommentQuery).Single();
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
            var groupPostComment = AmiqPostAutoMapper.Instance.ProjectTo<DtoGroupPostComment>(createdCommentQuery).Single();
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
            return AmiqPostAutoMapper.Instance.Map<DtoPostComment>(entity);
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
            return AmiqPostAutoMapper.Instance.Map<DtoPostComment>(entity);
        }
    }
}
