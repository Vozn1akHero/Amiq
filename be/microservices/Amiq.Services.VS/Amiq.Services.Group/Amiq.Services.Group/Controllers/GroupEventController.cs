using Amiq.Services.Base.Controllers;
using Amiq.Services.Common.Contracts;
using Amiq.Services.Group.BusinessLayer;
using Amiq.Services.Group.Contracts.Group;
using Microsoft.AspNetCore.Mvc;

namespace Amiq.Services.Group.Controllers
{
    //[Authorize]
    public class GroupEventController: AmiqBaseController
    {
        private BlGroupEvent bsGroupEvent = new BlGroupEvent();

        [HttpGet("list")]
        public async Task<IActionResult> GetEventsByGroupIdAsync([FromQuery] int groupId,
            [FromQuery] DtoPaginatedRequest dtoPaginatedRequest,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return new StatusCodeResult(499);
            }
            var data = await bsGroupEvent.GetAllGroupEventsAsync(groupId, dtoPaginatedRequest);
            return Ok(data);
        }

        [HttpGet("{groupEventId}")]
        [Produces(typeof(DtoGroupEvent))]
        public IActionResult GetEventById(Guid groupEventId)
        {
            var data = bsGroupEvent.GetEventByIdAsync(JwtStoredUserId, groupEventId);
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
