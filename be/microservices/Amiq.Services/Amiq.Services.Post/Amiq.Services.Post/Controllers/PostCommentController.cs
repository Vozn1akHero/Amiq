using Amiq.Services.Post.Base;
using Amiq.Services.Post.BusinessLayer.Post;
using Amiq.Services.Post.Common.Enums;
using Amiq.Services.Post.Contracts.Post;
using Amiq.Services.Post.Contracts.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Amiq.Services.Post.Controllers
{
    public class PostCommentController : AmiqBaseController
    {
        private BlPostComment bsPostComment = new BlPostComment();

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
        //[Authorize]
        public async Task<IActionResult> CreatePostComment([FromBody] DtoCreatePostComment dtoCreatePostComment)
        {
            var data = await bsPostComment.CreateAsync(JwtStoredUserId, dtoCreatePostComment);
            return CreatedAtAction(nameof(CreatePostComment), data);
        }

        [HttpPost("group-post-comment")]
        //[Authorize]
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

            var data = await bsPostComment.CreateGroupPostCommentAsync(JwtStoredUserId, dtoCreateGroupPostComment);
            return CreatedAtAction(nameof(CreateGroupPostComment), data);
        }
    }
}
