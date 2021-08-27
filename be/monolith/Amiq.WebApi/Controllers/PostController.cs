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
    [Route("api/post")]
    public class PostController : AmiqBaseController
    {
        [HttpGet("user/all/{userId}")]
        public async Task<IActionResult> GetPostsByUserId([FromRoute] int userId)
        {
            return await Task.FromResult(Ok());
        }

        [HttpGet("group/all/{groupId}")]
        public async Task<IActionResult> GetPostsByGroupId([FromRoute] int groupId)
        {
            return await Task.FromResult(Ok());
        }
    }
}
