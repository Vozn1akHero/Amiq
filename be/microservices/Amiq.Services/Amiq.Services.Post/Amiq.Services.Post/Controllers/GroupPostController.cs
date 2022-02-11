using Amiq.Services.Post.Base;
using Amiq.Services.Post.BusinessLayer.Post;
using Amiq.Services.Post.Contracts.Post;
using Amiq.Services.Post.Messaging;
using Amiq.Services.Post.Messaging.IntegrationEvents;
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

            var groupPostModificationEvent = new GroupPostModificationEvent(entity.GroupPostId, entity.PostId, entity.TextContent,
                entity.EditedBy, entity.EditedAt, entity.CreatedAt, entity.GroupId, entity.Author.UserId, entity.VisibleAsCreatedByAdmin, "C");
            RabbitMQPublisher.Publish<GroupPostModificationEvent>(groupPostModificationEvent);

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
