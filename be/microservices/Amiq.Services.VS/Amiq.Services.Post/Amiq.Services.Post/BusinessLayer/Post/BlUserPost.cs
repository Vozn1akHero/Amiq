using Amiq.Services.Post.Contracts.Post;
using Amiq.Services.Post.Contracts.Utils;
using Amiq.Services.Post.DataAccessLayer.Post;

namespace Amiq.Services.Post.BusinessLayer.Post
{
    public class BlUserPost
    {
        private DaoUserPost daUserPost = new DaoUserPost();

        public async Task<DtoListResponseOf<DtoUserPost>> GetUserPostsAsync(int userId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            return await daUserPost.GetUserPostsAsync(userId, dtoPaginatedRequest);
        }

        public async Task EditAsync(DtoEditUserPostRequest dtoEditUserPostRequest)
        {
            await daUserPost.EditAsync(dtoEditUserPostRequest);
        }

        public async Task<DtoUserPost> CreateAsync(int issuerId, DtoPostCreation dtoPost)
        {
            return await daUserPost.CreateAsync(issuerId, dtoPost);
        }
    }
}
