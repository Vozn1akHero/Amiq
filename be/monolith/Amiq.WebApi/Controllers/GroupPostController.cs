using Amiq.BusinessLayer.Post;
using Amiq.Contracts.Post;
using Amiq.WebApi.Base;
using Amiq.WebApi.Core.Auth;
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
    public class GroupPostController : AmiqBaseController
    {
        private BlGroupPost bsGroupPost = new BlGroupPost();

        [HttpGet("list")]
        public async Task<IActionResult> GetPostsByGroupIdAsync([FromQuery] DtoGroupPostRequest dtoGroupPostRequest, 
            CancellationToken cancellationToken = default)
        {
            if(dtoGroupPostRequest.Count > 10)
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
        //[AuthorizeGroupAdmin]
        public async Task<IActionResult> CreateGroupPostAsync([FromBody] DtoCreateGroupPost dtoCreateGroupPost)
        {
            var entity = await bsGroupPost.CreateAsync(dtoCreateGroupPost);
            return CreatedAtAction(nameof(CreateGroupPostAsync), entity);
        }

        [HttpPatch("edit")]
        //[AuthorizeGroupAdmin]
        public async Task<IActionResult> EditAsync([FromBody] DtoEditGroupPostRequest dtoEditGroupPostRequest)
        {
            await bsGroupPost.EditAsync(dtoEditGroupPostRequest);
            return Ok();
        }
    }
}
