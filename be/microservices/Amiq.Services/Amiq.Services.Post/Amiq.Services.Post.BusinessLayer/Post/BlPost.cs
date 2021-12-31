using Amiq.Contracts;
using Amiq.Contracts.Post;
using Amiq.DataAccessLayer.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.Post.BusinessLayer.Post
{
    public class BlPost
    {
        private DaoPost _daPost = new DaoPost();

        public async Task<DtoDeleteEntityResponse> DeleteAsync(Guid postId)
        {
            return await _daPost.DeleteByPostIdAsync(postId);
        }


    }
}
