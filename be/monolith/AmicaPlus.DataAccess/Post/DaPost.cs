using AmicaPlus.DataAccess.Models;
using AmicaPlus.DataAccess.Models.Models;
using AmicaPlus.ResultSets.Group;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmicaPlus.DataAccess.Post
{
    public class DaPost
    {
        private AmicaPlusContext _amicaPlusContext;

        public DaPost()
        {
            _amicaPlusContext = new AmicaPlusContext();
        }

        public async Task<List<RsGroupPost>> GetGroupPostsAsync(int groupId)
        {
            return await (from p in _amicaPlusContext.Posts.AsNoTracking()
                          join gp in _amicaPlusContext.GroupPosts.AsNoTracking()
                          on p.PostId equals gp.PostId
                          select new RsGroupPost { }).ToListAsync();
        }
    }
}
