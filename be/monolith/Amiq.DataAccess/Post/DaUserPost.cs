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
                .Skip(dtoPaginatedRequest.Skip).Take(dtoPaginatedRequest.Count);
            var data = await APAutoMapper.Instance.ProjectTo<DtoUserPost>(query).ToListAsync();
            return data;
        }
    }
}
