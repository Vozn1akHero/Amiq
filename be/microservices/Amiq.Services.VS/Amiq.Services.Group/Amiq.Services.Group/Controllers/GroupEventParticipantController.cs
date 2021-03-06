using Amiq.Services.Base.Controllers;
using Amiq.Services.Group.BusinessLayer;
using Microsoft.AspNetCore.Mvc;

namespace Amiq.Services.Group.Controllers
{
    public class GroupEventParticipantController : AmiqBaseController
    {
        private BlGroupEventParticipant _blGroupEventParticipant = new BlGroupEventParticipant();

        [HttpPost("join/{groupEventId}")]
        public async Task<IActionResult> JoinEventAsync(Guid groupEventId)
        {
            var result = await _blGroupEventParticipant.JoinEventAsync(JwtStoredUserId, groupEventId);
            return Ok(result);
        }

        [HttpDelete("leave/{groupEventId}")]
        public async Task<IActionResult> LeaveEventAsync(Guid groupEventId)
        {
            await _blGroupEventParticipant.LeaveGroupAsync(JwtStoredUserId, groupEventId);
            return Ok();
        }
    }
}
