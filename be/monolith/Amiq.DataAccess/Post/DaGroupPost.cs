using Amiq.Contracts.Post;
using Amiq.DataAccess.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amiq.DataAccess.Post
{
    public class DaGroupPost
    {
        private AmiqContext _amiqContext = new AmiqContext();

        public async Task<GroupPost> CreateAsync(DtoGroupPost dtoGroupPost)
        {
            var entity = new GroupPost
            {
                GroupId = dtoGroupPost.GroupId,
                Post = new DataAccess.Models.Models.Post
                {
                    TextContent = dtoGroupPost.TextContent
                }
            };
            _amiqContext.GroupPosts.Add(entity);
            await _amiqContext.SaveChangesAsync();
            return entity;
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

        public async Task<IEnumerable<DtoGroupPost>> GetGroupPostsAsync(DtoGroupPostRequest dtoGroupPostRequest)
        {
            return await (from p in _amiqContext.Posts.AsNoTracking()
                          join gp in _amiqContext.GroupPosts.AsNoTracking()
                          on p.PostId equals gp.PostId
                          where gp.GroupId == dtoGroupPostRequest.GroupId
                          select new DtoGroupPost {
                            GroupId = gp.GroupId,
                            TextContent = p.TextContent,
                            AuthorId = p.
                          })
                          .Skip(dtoGroupPostRequest.Skip)
                          .Take(dtoGroupPostRequest.Count)
                          .ToListAsync();
        }
    }
}
