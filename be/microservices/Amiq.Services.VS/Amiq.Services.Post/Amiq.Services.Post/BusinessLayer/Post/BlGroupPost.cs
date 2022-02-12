using Amiq.Services.Common.Contracts;
using Amiq.Services.Post.BusinessLayer.Utils;
using Amiq.Services.Post.Contracts.Post;
using Amiq.Services.Post.DataAccessLayer.Post;

namespace Amiq.Services.Post.BusinessLayer.Post
{
    public class BlGroupPost : BusinessLayerBase
    {
        private DaoGroupPost _daGroupPost = new DaoGroupPost();

        public async Task<DtoGroupPost> CreateAsync(DtoCreateGroupPost dtoCreateGroupPost)
        {
            return await _daGroupPost.CreateAsync(dtoCreateGroupPost);
        }

        public async Task EditAsync(DtoEditGroupPostRequest dtoEditGroupPostRequest)
        {
            await _daGroupPost.EditAsync(dtoEditGroupPostRequest);
        }

        public async Task<DtoListResponseOf<DtoGroupPost>> GetGroupPostsAsync(DtoGroupPostRequest dtoGroupPostRequest)
        {
            return await _daGroupPost.GetGroupPostsAsync(dtoGroupPostRequest);
        }
    }
}
