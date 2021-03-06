using Amiq.Contracts.Post;
using Amiq.Contracts.Utils;
using Amiq.DataAccessLayer.Models.Models;
using Amiq.DataAccessLayer.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.BusinessLayer.Post
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
