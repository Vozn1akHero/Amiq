using AmicaPlus.WebApi.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmicaPlus.WebApi.Controllers
{
    [Route("api/post")]
    public class PostController : AmicaPlusBaseController
    {
        [HttpGet("user/all")]
        public async Task<IActionResult> GetPostsByUserId([FromQuery] Guid userId)
        {
            return await Task.FromResult(Ok());
        }
    }
}
