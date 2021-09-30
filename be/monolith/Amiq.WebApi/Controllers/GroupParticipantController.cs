using Amiq.Business;
using Amiq.Contracts.Group;
using Amiq.Contracts.Utils;
using Amiq.Mapping;
using Amiq.WebApi.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    public class GroupParticipantController : AmiqBaseController
    {
        private BsGroupParticipant _bsGroupParticipant = new BsGroupParticipant();

        [HttpGet("user-groups/{userId}")]
        public async Task<ActionResult<List<DtoGroup>>> GetUserGroupsByUserIdAsync(int userId, 
            [FromQuery] DtoPaginatedRequest dtoPaginatedRequest)
        {
            var groups = await _bsGroupParticipant.GetUserGroupsByUserIdAsync(userId, dtoPaginatedRequest);
            return Ok(groups);
        }

        [HttpDelete("leave")]
        public async Task<IActionResult> LeaveGroupAsync(DtoLeaveGroup dtoLeaveGroup)
        {
            await _bsGroupParticipant.LeaveGroupAsync(dtoLeaveGroup);
            return Ok();
        }

        [HttpPost("join")]
        public async Task<IActionResult> JoinGroupAsync([FromBody] DtoJoinGroup dtoJoinGroup)
        {
            await _bsGroupParticipant.JoinGroupAsync(dtoJoinGroup);
            return Ok();
        }

        [HttpPost("is-participant")]
        public async Task<IActionResult> GetGroupParticipantAsync([FromQuery] DtoMinifiedGroupParticipant dtoMinifiedGroupParticipant)
        {
            var groupParticipants = await _bsGroupParticipant.GetGroupParticipantAsync(dtoMinifiedGroupParticipant);
            return Ok(groupParticipants);
        }
    }
}
