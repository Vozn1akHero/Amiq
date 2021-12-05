using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Amiq.Services.BusinessLayer;
using Amiq.Services.Group.Contracts;
using Amiq.Services.Group.Contracts.Utils;
using Amiq.Services.Group.Contracts.Enums;

namespace Amiq.Services.Group.Controllers
{
    [Authorize]
    public class GroupParticipantController : ControllerBase
    {
        private BlGroupParticipant _bsGroupParticipant = new BlGroupParticipant();

        [HttpGet("user-groups")]
        public async Task<ActionResult<DtoListResponseOf<DtoGroup>>> GetUserGroupsByUserIdAsync(int userId, [FromQuery] EnGroupFilterType filterType,
            [FromQuery] DtoPaginatedRequest dtoPaginatedRequest,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return new StatusCodeResult(499);
            }

            var groups = await _bsGroupParticipant.GetUserGroupsByUserIdAsync(userId, dtoPaginatedRequest, filterType);
            return Ok(groups);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteParticipantAsync([FromQuery] int userId, [FromQuery] int groupId)
        {
            await _bsGroupParticipant.DeleteParticipantAsync(userId, groupId);
            return Ok();
        }

        [HttpDelete("leave")]
        public async Task<IActionResult> LeaveGroupAsync(int userId, [FromQuery] int groupId)
        {
            await _bsGroupParticipant.LeaveGroupAsync(userId, groupId);
            return Ok();
        }

        [HttpPost("join/{groupId}")]
        public async Task<IActionResult> JoinGroupAsync(int userId, [FromRoute] int groupId)
        {
            await _bsGroupParticipant.JoinGroupAsync(userId, groupId);
            return Ok();
        }

        [HttpPost("is-participant")]
        public async Task<IActionResult> GetGroupParticipantAsync([FromQuery] DtoMinifiedGroupParticipant dtoMinifiedGroupParticipant)
        {
            var groupParticipants = await _bsGroupParticipant.GetGroupParticipantAsync(dtoMinifiedGroupParticipant);
            return Ok(groupParticipants);
        }

        [HttpGet("viewer-role")]
        [Authorize]
        public async Task<IActionResult> GetGroupRoleAsync([FromQuery] int userId, [FromQuery] int groupId)
        {
            var res = await _bsGroupParticipant.GetGroupViewerByUserIdAsync(userId, groupId);
            return Ok(res);
        }

        [HttpGet("list/{groupId}")]
        [Authorize]
        public async Task<IActionResult> GetParticipantsAsync([FromRoute] int groupId, [FromQuery] DtoPaginatedRequest dtoPaginatedRequest)
        {
            var res = await _bsGroupParticipant.GetGroupParticipantsAsync(groupId, dtoPaginatedRequest);
            return Ok(res);
        }
        
    }
}
