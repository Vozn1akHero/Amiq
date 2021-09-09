using Amiq.Business.Post;
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
        private BsGroupPost bsGroupPost = new BsGroupPost();

        [HttpGet("list")]
        public async Task<IActionResult> GetPostsByGroupIdAsync([FromQuery] DtoGroupPostRequest dtoGroupPostRequest, 
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return new StatusCodeResult(CLIENT_CLOSED_REQUEST_STATUS_CODE);
            }
            var data = await bsGroupPost.GetGroupPostsAsync(dtoGroupPostRequest);
            return Ok(data);
        }

        [HttpPost]
        [AuthorizeGroupAdmin]
        public async Task<IActionResult> CreateGroupPostAsync([FromBody] DtoGroupPost dtoGroupPost)
        {
            var entity = await bsGroupPost.CreateAsync(dtoGroupPost);
            return CreatedAtAction(nameof(CreateGroupPostAsync), entity);
        }
    }
}
