using Amiq.Business.Post;
using Amiq.Common.Enums;
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
    public class PostCommentController : AmiqBaseController
    {
        private BsPostComment bsPostComment = new BsPostComment();

        [HttpGet("user-post-comments")]
        public async Task<IActionResult> GetUserPostCommentsAsync([FromQuery] Guid postId,
            [FromQuery] DtoPaginatedRequest dtoPaginatedRequest,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return new StatusCodeResult(CLIENT_CLOSED_REQUEST_STATUS_CODE);
            }
            var data = await bsPostComment.GetUserPostCommentsAsync(postId, dtoPaginatedRequest);
            return Ok(data);
        }

        [HttpGet("group-post-comments")]
        public async Task<IActionResult> GetGroupPostCommentsByGroupIdAsync([FromQuery] Guid postId,
            [FromQuery] DtoPaginatedRequest dtoPaginatedRequest,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return new StatusCodeResult(CLIENT_CLOSED_REQUEST_STATUS_CODE);
            }
            var data = await bsPostComment.GetGroupPostCommentsAsync(postId, dtoPaginatedRequest);
            return Ok(data);
        }

        [HttpDelete("{postCommentId}")]
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePostComment([FromBody] DtoCreatePostComment dtoCreatePostComment)
        {
            var data = await bsPostComment.CreateAsync(JwtStoredUserInfo.UserId, dtoCreatePostComment);
            return CreatedAtAction(nameof(CreatePostComment), data); 
        }

        [HttpPost("group-post-comment")]
        [Authorize]
        public async Task<IActionResult> CreateGroupPostComment([FromBody] DtoCreateGroupPostComment dtoCreateGroupPostComment)
        {
            try
            {
                dtoCreateGroupPostComment.AuthorVisibilityType = !string.IsNullOrEmpty(dtoCreateGroupPostComment.AuthorVisibilityType) ?
                    EnumExtensions.TryMapStrValueToAltValue(typeof(EnCommentAuthorVisibilityType), dtoCreateGroupPostComment.AuthorVisibilityType) :
                    EnCommentAuthorVisibilityType.User.GetEnumAltValue();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var data = await bsPostComment.CreateGroupPostCommentAsync(JwtStoredUserInfo.UserId, dtoCreateGroupPostComment);
            return CreatedAtAction(nameof(CreateGroupPostComment), data);
        }
    }
}
