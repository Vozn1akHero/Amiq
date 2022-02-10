using Amiq.BusinessLayer.Post;
using Amiq.Contracts.Post;
using Amiq.DataAccessLayer.Post;
using Amiq.WebApi.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    [Authorize]
    public class PostController : AmiqBaseController
    {
        private BlPost bsPost = new BlPost();

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid postId)
        {
            await bsPost.DeleteAsync(postId);
            return Ok();
        }

       
    }
}
