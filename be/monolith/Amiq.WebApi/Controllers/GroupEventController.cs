using Amiq.Business.Group;
using Amiq.Contracts.Utils;
using Amiq.WebApi.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    [Authorize]
    public class GroupEventController:AmiqBaseController
    {
        private BsGroupEvent bsGroupEvent = new BsGroupEvent();

        [HttpGet("all")]
        public async Task<IActionResult> GetEventsByGroupIdAsync([FromQuery] int groupId, CancellationToken cancellationToken = default)
        {
            var data = await bsGroupEvent.GetAllGroupEventsAsync(groupId);
            return Ok(data);
        }

        [HttpPut("cancel")]
        public async Task<IActionResult> CancelEventAsync([FromQuery] int groupId, [FromQuery] Guid groupEventId)
        {
            var result = await bsGroupEvent.CancelEventAsync(groupId, groupEventId);
            return Ok(result);
        }

        [HttpPut("reopen")]
        public async Task<IActionResult> ReopenEventAsync([FromQuery] int groupId, [FromQuery] Guid groupEventId)
        {
            var result = await bsGroupEvent.ReopenEventAsync(groupId, groupEventId);
            return Ok(result);
        }

        [HttpPut("visibility")]
        public async Task<IActionResult> SetEventVisibilityAsync([FromQuery] int groupId, [FromQuery] Guid groupEventId, [FromQuery] int isVisible)
        {
            bool isVisibleBl = isVisible == 1 ? true : false;
            var result = await bsGroupEvent.SetEventVisibilityAsync(groupId, groupEventId, isVisibleBl);
            return Ok(result);
        }
    }
}
