using Amiq.Business.Utils;
using Amiq.Contracts;
using Amiq.Contracts.Post;
using Amiq.DataAccess.Models.Models;
using Amiq.DataAccess.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business.Post
{
    public class BlGroupPost : BusinessLayerBase
    {
        private DaoGroupPost _daGroupPost = new DaoGroupPost();

        public async Task<DtoGroupPost> CreateAsync(DtoGroupPost dtoGroupPost)
        {
            return await _daGroupPost.CreateAsync(dtoGroupPost);
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
