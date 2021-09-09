using Amiq.Contracts.Post;
using Amiq.Contracts.Utils;
using Amiq.DataAccess.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business.Post
{
    public class BsUserPost
    {
        private DaUserPost daUserPost = new DaUserPost();

        public async Task<IEnumerable<DtoUserPost>> GetUserPostsAsync(int userId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            return await daUserPost.
        }
    }
}
