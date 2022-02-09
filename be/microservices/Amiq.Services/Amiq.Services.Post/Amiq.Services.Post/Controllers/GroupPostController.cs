using Amiq.Services.Post.BusinessLayer.Post;
using Amiq.Services.Post.Contracts.Post;
using Amiq.Services.User.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Amiq.Services.Post.Controllers
{
    [Authorize]
    public class GroupPostController : AmiqBaseController
    {
        private BlGroupPost bsGroupPost = new BlGroupPost();

        [HttpGet("list")]
        public async Task<IActionResult> GetPostsByGroupIdAsync([FromQuery] DtoGroupPostRequest dtoGroupPostRequest,
            CancellationToken cancellationToken = default)
        {
            if (dtoGroupPostRequest.Count > 10)
            {
                return BadRequest();
            }
            if (cancellationToken.IsCancellationRequested)
            {
                return new StatusCodeResult(CLIENT_CLOSED_REQUEST_STATUS_CODE);
            }
            var data = await bsGroupPost.GetGroupPostsAsync(dtoGroupPostRequest);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroupPostAsync([FromBody] DtoCreateGroupPost dtoCreateGroupPost)
        {
            var entity = await bsGroupPost.CreateAsync(dtoCreateGroupPost);
            return CreatedAtAction(nameof(CreateGroupPostAsync), entity);
        }

        [HttpPatch("edit")]
        public async Task<IActionResult> EditAsync([FromBody] DtoEditGroupPostRequest dtoEditGroupPostRequest)
        {
            await bsGroupPost.EditAsync(dtoEditGroupPostRequest);
            return Ok();
        }
    }
}
