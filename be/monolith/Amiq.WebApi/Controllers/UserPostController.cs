using Amiq.Business.Post;
using Amiq.Contracts.Utils;
using Amiq.WebApi.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    [Authorize]
    [Route("api/user-post")]
    public class UserPostController : AmiqBaseController
    {
        private BsUserPost bsUserPost = new BsUserPost();

        [HttpGet("list/{userId}")]
        public async Task<IActionResult> GetPostsByUserId([FromRoute] int userId, [FromQuery] DtoPaginatedRequest dtoPaginatedRequest)
        {
            var data = await bsUserPost.GetUserPostsAsync(userId, dtoPaginatedRequest);
            return Ok(data);
        }

        
    }
}
