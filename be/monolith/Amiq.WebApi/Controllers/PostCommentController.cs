using Amiq.Business.Post;
using Amiq.Contracts.Post;
using Amiq.Contracts.Utils;
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
    //[Authorize]
    public class PostCommentController : AmiqBaseController
    {
        private BsPostComment bsPostComment = new BsPostComment();

        [HttpGet("list")]
        public async Task<IActionResult> GetPostCommentsByGroupIdAsync([FromQuery] Guid postId,
            [FromQuery] DtoPaginatedRequest dtoPaginatedRequest,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return new StatusCodeResult(CLIENT_CLOSED_REQUEST_STATUS_CODE);
            }
            var data = await bsPostComment.GetPostCommentsAsync(postId, dtoPaginatedRequest);
            return Ok(data);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> DeletePostCommentAsync(Guid postCommentId)
        {
            var data = await bsPostComment.DeleteAsync(postCommentId);
            return Ok(data);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditPostCommentAsync(Guid postCommentId, [FromQuery] string text)
        {
            var data = await bsPostComment.EditAsync(postCommentId, text);
            return Ok(data);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] DtoNewPostComment newPostComment)
        {
            var data = await bsPostComment.CreateAsync(newPostComment);
            return CreatedAtAction(nameof(CreateAsync), data);
        }
    }
}
