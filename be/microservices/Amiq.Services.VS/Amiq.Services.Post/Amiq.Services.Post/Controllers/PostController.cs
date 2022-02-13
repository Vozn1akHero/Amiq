using Amiq.Services.Base.Controllers;
using Amiq.Services.Post.BusinessLayer.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Amiq.Services.Post.Controllers
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
