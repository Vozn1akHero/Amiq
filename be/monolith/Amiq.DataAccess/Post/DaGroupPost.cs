using Amiq.Contracts.Post;
using Amiq.DataAccess.Models.Models;
using Amiq.Mapping;
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

        public async Task<DtoGroupPost> CreateAsync(DtoGroupPost dtoGroupPost)
        {
            var entity = new GroupPost
            {
                GroupId = dtoGroupPost.GroupId,
                AuthorId = dtoGroupPost.Author.UserId,
                Post = new Models.Models.Post
                {
                    TextContent = dtoGroupPost.TextContent
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

        public async Task<IEnumerable<DtoGroupPost>> GetGroupPostsAsync(DtoGroupPostRequest dtoGroupPostRequest)
        {
            var query = (from p in _amiqContext.Posts.AsNoTracking()
                         join gp in _amiqContext.GroupPosts.AsNoTracking()
                         on p.PostId equals gp.PostId
                         where gp.GroupId == dtoGroupPostRequest.GroupId
                         select gp)
                         .Skip((dtoGroupPostRequest.Page - 1) * dtoGroupPostRequest.Count)
                         .Take(dtoGroupPostRequest.Count);
            var data = await APAutoMapper.Instance.ProjectTo<DtoGroupPost>(query).ToListAsync();
            return data;
        }
    }
}
