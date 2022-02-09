using Amiq.Services.Post.Contracts.Utils;
using Amiq.Services.Post.DataAccessLayer.Post;

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
