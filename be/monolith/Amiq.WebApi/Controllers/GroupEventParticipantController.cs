using Amiq.BusinessLayer.GroupEvent;
using Amiq.WebApi.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    public class GroupEventParticipantController : AmiqBaseController
    {
        private BlGroupEventParticipant _blGroupEventParticipant = new BlGroupEventParticipant();

        [HttpPost("join/{groupEventId}")]
        public async Task<IActionResult> JoinEventAsync(Guid groupEventId)
        {
            var result = await _blGroupEventParticipant.JoinEventAsync(JwtStoredUserInfo.UserId, groupEventId);
            return Ok(result);
        }

        [HttpDelete("leave/{groupEventId}")]
        public async Task<IActionResult> LeaveEventAsync(Guid groupEventId)
        {
            await _blGroupEventParticipant.LeaveGroupAsync(JwtStoredUserInfo.UserId, groupEventId);
            return Ok();
        }
    }
}
