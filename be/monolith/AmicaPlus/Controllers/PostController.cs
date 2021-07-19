using AmicaPlus.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmicaPlus.Controllers
{
    [Route("[controller]")]
    public class PostController : AmicaPlusBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetPostsByUserId()
        {
            return await Task.FromResult(Ok());
        }
    }
}
