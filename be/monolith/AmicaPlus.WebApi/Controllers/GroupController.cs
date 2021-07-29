using AmicaPlus.WebApi.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmicaPlus.WebApi.Controllers
{
    public class GroupController : AmicaPlusBaseController
    {
        [HttpGet("user")]
        public async Task<IActionResult> GetGroupsByUserId([FromQuery] Guid participantId)
        {
            return await Task.FromResult(Ok());
        }
    }
}
