using Amiq.Business.Utils;
using Amiq.Contracts;
using Amiq.Contracts.Post;
using Amiq.DataAccessLayer.Models.Models;
using Amiq.DataAccessLayer.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
