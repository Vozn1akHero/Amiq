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
    public class DaUserPost
    {
        private AmiqContext _amiqContext = new AmiqContext();

        public async Task<IEnumerable<DtoUserPost>> GetUserPostsAsync(int userId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            var query = _amiqContext.UserPosts.Where(e=>e.UserId == userId)
                .Skip((dtoPaginatedRequest.Page - 1) * dtoPaginatedRequest.Count)
                .Take(dtoPaginatedRequest.Count)
                .OrderByDescending(e=>e.Post.CreatedAt)
                .Include(e=>e.Post).Include(e=>e.Post.Comments);
            var data = await APAutoMapper.Instance.ProjectTo<DtoUserPost>(query).ToListAsync();
            return data;
        }

        public async Task EditAsync(DtoEditUserPostRequest dtoEditUserPostRequest)
        {
            var post = _amiqContext.UserPosts.Include(e=>e.Post).SingleOrDefault(e=>e.PostId == dtoEditUserPostRequest.PostId);
            if(post != null)
            {
                post.Post.TextContent = dtoEditUserPostRequest.TextContent;
                post.Post.EditedAt = DateTime.Now;
                post.Post.EditedBy = dtoEditUserPostRequest.UserId;
                await _amiqContext.SaveChangesAsync();
            }
        }

        public async Task<DtoUserPost> CreateAsync(int issuerId, DtoPostCreation dtoPost)
        {
            var userPost = APAutoMapper.Instance.Map<UserPost>(dtoPost);
            var post = APAutoMapper.Instance.Map<Models.Models.Post>(dtoPost);
            userPost.UserId = issuerId;
            _amiqContext.Posts.Add(post);
            await _amiqContext.SaveChangesAsync();

            userPost.PostId = post.PostId;
            _amiqContext.UserPosts.Add(userPost);
            await _amiqContext.SaveChangesAsync();

            var query = _amiqContext.UserPosts.Where(e => e.PostId == userPost.PostId)
                .Include(e => e.Post)
                .Include(e => e.Post.Comments);
            var query2 = _amiqContext.UserPosts.Where(e => e.PostId == userPost.PostId)
                .Include(e => e.Post)
                .Include(e => e.Post.Comments).Single();
            var res = APAutoMapper.Instance.ProjectTo<DtoUserPost>(query).Single();
            return res;
        }
    }
}
