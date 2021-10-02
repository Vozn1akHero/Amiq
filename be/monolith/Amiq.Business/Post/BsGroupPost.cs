using Amiq.Business.Utils;
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
    public class BsGroupPost : BsServiceBase
    {
        private DaGroupPost _daGroupPost = new DaGroupPost();

        public async Task<DtoGroupPost> CreateAsync(DtoGroupPost dtoGroupPost)
        {
            return await _daGroupPost.CreateAsync(dtoGroupPost);
        }

        public async Task EditAsync(DtoEditGroupPostRequest dtoEditGroupPostRequest)
        {
            await _daGroupPost.EditAsync(dtoEditGroupPostRequest);
        }

        public async Task<IEnumerable<DtoGroupPost>> GetGroupPostsAsync(DtoGroupPostRequest dtoGroupPostRequest)
        {
            return await _daGroupPost.GetGroupPostsAsync(dtoGroupPostRequest);
        }
    }
}
