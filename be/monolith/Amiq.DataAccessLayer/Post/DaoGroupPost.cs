using Amiq.Common;
using Amiq.Contracts.Group;
using Amiq.Contracts.Post;
using Amiq.Contracts.User;
using Amiq.Contracts.Utils;
using Amiq.DataAccessLayer.Models;
using Amiq.DataAccessLayer.Models.Models;
using Amiq.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Amiq.DataAccessLayer.Post
{
    public class DaoGroupPost
    {
        private AmiqContext _amiqContext = new AmiqContext();
        private AmiqContextWithDebugLogging _amiqContextWithDebugLogging = new AmiqContextWithDebugLogging();

        public async Task<DtoGroupPost> CreateAsync(DtoCreateGroupPost dtoCreateGroupPost)
        {
            var entity = new GroupPost
            {
                GroupId = dtoCreateGroupPost.GroupId,
                AuthorId = dtoCreateGroupPost.Author.UserId,
                VisibleAsCreatedByAdmin = dtoCreateGroupPost.CreateAsAdmin,
                Post = new Models.Models.Post
                {
                    TextContent = dtoCreateGroupPost.TextContent
                }
            };
            _amiqContext.GroupPosts.Add(entity);
            await _amiqContext.SaveChangesAsync();
            //var query = _amiqContext.GroupPosts.Where(e => e.PostId == entity.PostId);
            var query = (from p in _amiqContext.Posts.AsNoTracking()
                         join gp in _amiqContext.GroupPosts.AsNoTracking()
                         on p.PostId equals gp.PostId
                         where gp.PostId == entity.PostId
                         select gp);
            var mappedDto = APAutoMapper.Instance.ProjectTo<DtoGroupPost>(query).Single();
            return mappedDto;
        }

        public async Task EditAsync(DtoEditGroupPostRequest dtoEditGroupPostRequest)
        {
            var post = _amiqContext.GroupPosts.SingleOrDefault(e => e.GroupPostId == dtoEditGroupPostRequest.GroupPostId);
            if (post != null)
            {
                post.Post.TextContent = dtoEditGroupPostRequest.Text;
                //TODO
                await _amiqContext.SaveChangesAsync();
            }
        }

        public async Task<DtoListResponseOf<DtoGroupPost>> GetGroupPostsAsync(DtoGroupPostRequest dtoGroupPostRequest)
        {
            DtoListResponseOf<DtoGroupPost> result = new();
/*            const int TAKE = 5;
            const int PAGE = 1;*/

            result.Length = await _amiqContextWithDebugLogging.GroupPosts.AsNoTracking()
                .Where(e => e.GroupId == dtoGroupPostRequest.GroupId).CountAsync();

            var query = _amiqContextWithDebugLogging.GroupPosts.AsNoTracking()
                         .Include(e => e.Post)
                         .Where(e=>e.GroupId == dtoGroupPostRequest.GroupId)
                         .OrderByDescending(e=>e.Post.CreatedAt)
                         .Skip((dtoGroupPostRequest.Page - 1) * dtoGroupPostRequest.Count)
                         .Take(dtoGroupPostRequest.Count);
            var data = await APAutoMapper.Instance.ProjectTo<DtoGroupPost>(query).ToListAsync();
            
            /*foreach (var item in data)
            {
                if(item.CommentsCount > 0)
                {
                    var recentCommentsQuery = _amiqContextWithDebugLogging.GroupPostComments.AsNoTracking()
                        .Where(e => e.Comment.PostId == item.PostId && !e.Comment.ParentId.HasValue)
                        .Include(e => e.Comment.Author)
                        .Paginate(PAGE, TAKE)
                        .OrderByDescending(e => e.Comment.CreatedAt);
                    var comments = await APAutoMapper.Instance.ProjectTo<DtoGroupPostComment>(recentCommentsQuery).ToListAsync();
                    item.Comments = comments;
                    item.HasMoreCommentsThanRecent = item.CommentsCount > item.Comments.Count;
                }
            }*/

            result.Entities = data; 
            return result;
        }
    }
}
