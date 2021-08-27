using Amiq.Contracts.Group;
using Amiq.WebApi.Base;
using Amiq.WebApi.Core.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    public class GroupController : AmiqBaseController
    {
        [HttpGet("user")]
        public async Task<IActionResult> GetGroupsByUserId([FromQuery] Guid participantId)
        {
            return await Task.FromResult(Ok());
        }

        [HttpPost("drop")]
        [AuthorizeMainGroupAdmin]
        public async Task<IActionResult> DropGroup([FromBody] DtoDropGroupRequest dtoDropGroupRequest)
        {
            return await Task.FromResult(Ok());
        }
    }
}
