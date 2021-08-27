using AmicaPlus.Business;
using AmicaPlus.Contracts.Group;
using AmicaPlus.Mapping;
using AmicaPlus.WebApi.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmicaPlus.WebApi.Controllers
{
    public class GroupParticipantController : AmicaPlusBaseController
    {
        private BsGroupParticipant _bsGroupParticipant = new BsGroupParticipant();

        [HttpGet("user-groups/{userId}")]
        public async Task<ActionResult<List<DtoGroup>>> GetUserGroupsByUserIdAsync(int userId)
        {
            var groups = await _bsGroupParticipant.GetUserGroupsByUserIdAsync(userId);
            var groupsDto = APAutoMapper.Instance.Map<List<DtoGroup>>(groups);
            return Ok(groupsDto);
        }

        [HttpDelete("leave")]
        public async Task<IActionResult> LeaveGroupAsync(DtoLeaveGroup dtoLeaveGroup)
        {
            var rsLeaveGroup = APAutoMapper.Instance.Map<DtoLeaveGroup>(dtoLeaveGroup);
            await _bsGroupParticipant.LeaveGroupAsync(rsLeaveGroup);
            return Ok();
        }

        [HttpPost("join")]
        public async Task<IActionResult> JoinGroupAsync([FromBody] DtoJoinGroup dtoJoinGroup)
        {
            var rsJoinGroup = APAutoMapper.Instance.Map<DtoJoinGroup>(dtoJoinGroup);
            await _bsGroupParticipant.JoinGroupAsync(rsJoinGroup);
            return Ok();
        }

        [HttpPost("is-participant")]
        public async Task<IActionResult> GetGroupParticipantAsync([FromQuery] DtoMinifiedGroupParticipant dtoMinifiedGroupParticipant)
        {
            var rsMinifiedGroupParticipant = APAutoMapper.Instance.Map<DtoMinifiedGroupParticipant>(dtoMinifiedGroupParticipant);
            var groupParticipants = await _bsGroupParticipant.GetGroupParticipantAsync(rsMinifiedGroupParticipant);
            var rsGroupParticipants = APAutoMapper.Instance.Map<List<DtoGroupParticipant>>(groupParticipants);
            return Ok(rsGroupParticipants);
        }
    }
}
