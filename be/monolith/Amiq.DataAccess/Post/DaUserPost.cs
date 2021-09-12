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
                .Take(dtoPaginatedRequest.Count);
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
    }
}
