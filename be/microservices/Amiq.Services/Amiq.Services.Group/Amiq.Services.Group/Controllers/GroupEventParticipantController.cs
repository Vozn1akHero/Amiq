using Amiq.Services.BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Amiq.Services.Group.Contracts;
using Amiq.Services.Group.Contracts.Utils;

namespace Amiq.Services.Group.Controllers
{
    public class GroupEventParticipantController : ControllerBase
    {
        private BlGroupEventParticipant _blGroupEventParticipant = new BlGroupEventParticipant();

        [HttpPost("join/{groupEventId}")]
        public async Task<IActionResult> JoinEventAsync(int userId, Guid groupEventId)
        {
            var result = await _blGroupEventParticipant.JoinEventAsync(userId, groupEventId);
            return Ok(result);
        }

        [HttpDelete("leave/{groupEventId}")]
        public async Task<IActionResult> LeaveEventAsync(int userId, Guid groupEventId)
        {
            await _blGroupEventParticipant.LeaveGroupAsync(userId, groupEventId);
            return Ok();
        }
    }
}
