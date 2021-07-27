using AmicaPlus.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmicaPlus.Controllers
{
    public class UserController : AmicaPlusBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetPostsByUserId(Guid userId)
        {
            return await Task.FromResult(Ok());
        }
    }
}
