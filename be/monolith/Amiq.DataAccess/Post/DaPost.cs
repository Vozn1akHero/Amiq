using Amiq.Contracts.Post;
using Amiq.DataAccess.Models;
using Amiq.DataAccess.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccess.Post
{
    public class DaPost
    {
        private AmiqContext _AmiqContext;

        public DaPost()
        {
            _AmiqContext = new AmiqContext();
        }

        public async Task<List<DtoGroupPost>> GetGroupPostsAsync(int groupId)
        {
            return await (from p in _AmiqContext.Posts.AsNoTracking()
                          join gp in _AmiqContext.GroupPosts.AsNoTracking()
                          on p.PostId equals gp.PostId
                          select new DtoGroupPost { }).ToListAsync();
        }
    }
}
