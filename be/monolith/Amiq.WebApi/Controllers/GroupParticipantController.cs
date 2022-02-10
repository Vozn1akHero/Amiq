using Amiq.BusinessLayer.Group;
using Amiq.Contracts.Group;
using Amiq.Contracts.Group.Enums;
using Amiq.Contracts.Utils;
using Amiq.Mapping;
using Amiq.WebApi.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    [Authorize]
    public class GroupParticipantController : AmiqBaseController
    {
        private BlGroupParticipant _bsGroupParticipant = new BlGroupParticipant();

        [HttpGet("user-groups")]
        public async Task<ActionResult<DtoListResponseOf<DtoGroup>>> GetUserGroupsByUserIdAsync([FromQuery] EnGroupFilterType filterType,
            [FromQuery] DtoPaginatedRequest dtoPaginatedRequest,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return new StatusCodeResult(CLIENT_CLOSED_REQUEST_STATUS_CODE);
            }

            var groups = await _bsGroupParticipant.GetUserGroupsByUserIdAsync(JwtStoredUserInfo.UserId, dtoPaginatedRequest, filterType);
            return Ok(groups);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteParticipantAsync([FromQuery] int userId, [FromQuery] int groupId)
        {
            await _bsGroupParticipant.DeleteParticipantAsync(userId, groupId);
            return Ok();
        }

        [HttpDelete("leave")]
        public async Task<IActionResult> LeaveGroupAsync([FromQuery] int groupId)
        {
            await _bsGroupParticipant.LeaveGroupAsync(JwtStoredUserInfo.UserId, groupId);
            return Ok();
        }

        [HttpPost("join/{groupId}")]
        public async Task<IActionResult> JoinGroupAsync([FromRoute] int groupId)
        {
            await _bsGroupParticipant.JoinGroupAsync(JwtStoredUserInfo.UserId, groupId);
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
