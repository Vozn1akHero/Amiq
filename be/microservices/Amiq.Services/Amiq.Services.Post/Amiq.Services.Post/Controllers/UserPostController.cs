using Amiq.Services.Post.BusinessLayer.Post;
using Amiq.Services.Post.BusinessLayer.Utils;
using Amiq.Services.Post.Contracts.Post;
using Amiq.Services.Post.Contracts.Utils;
using Amiq.Services.User.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Amiq.Services.Post.Controllers
{
    [Authorize]
    [Route("api/user-post")]
    public class UserPostController : AmiqBaseController
    {
        private BlUserPost bsUserPost = new BlUserPost();

        [HttpGet("list/{userId}")]
        public async Task<IActionResult> GetPostsByUserId([FromRoute] int userId, [FromQuery] DtoPaginatedRequest dtoPaginatedRequest)
        {
            var data = await bsUserPost.GetUserPostsAsync(userId, dtoPaginatedRequest);
            return Ok(data);
        }

        [HttpPatch]
        public async Task<IActionResult> EditAsync([FromBody] DtoEditUserPostRequest dtoEditUserPostRequest)
        {
            await bsUserPost.EditAsync(dtoEditUserPostRequest);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] DtoPostCreation dtoPost)
        {
            try
            {
                var data = await bsUserPost.CreateAsync(JwtStoredUserId, dtoPost);
                return CreatedAtAction(nameof(CreateAsync), data);
            }
            catch (BsRuleIsBrokenException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}
