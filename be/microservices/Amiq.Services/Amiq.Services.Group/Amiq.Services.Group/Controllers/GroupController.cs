﻿using Amiq.Services.BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Amiq.Services.Group.Contracts;
using Amiq.Services.Group.Contracts.Utils;
using Amiq.Services.Group.Base;

namespace Amiq.Services.Group.Controllers
{
    //[Authorize]
    public class GroupController : AmiqGroupBaseController
    {
        private BlGroup bsGroup = new BlGroup();

        [HttpPost]
        public async Task<IActionResult> CreateGroupAsync([FromBody] DtoCreateGroup dtoCreateGroup)
        {
            DtoGroupCard group = await bsGroup.CreateGroupAsync(JwtStoredUserId, dtoCreateGroup);
            return CreatedAtAction(nameof(CreateGroupAsync), group);
        }

        [HttpPost("drop")]
        public async Task<IActionResult> DropGroup([FromBody] DtoDropGroupRequest dtoDropGroupRequest)
        {
            return await Task.FromResult(Ok());
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var data = await bsGroup.GetByName(JwtStoredUserId, name);
            return Ok(data);
        }

        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetGroupById(int groupId)
        {
            var data = await bsGroup.GetGroupById(groupId);
            if(data == null) return NotFound();
            return Ok(data);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditBasicDataAsync([FromBody] DtoEditGroupDataRequest dtoEditGroupDataRequest)
        {
            var result = await bsGroup.EditAsync(dtoEditGroupDataRequest);
            return Ok(result);
        }

        [HttpGet("user-params/{groupId}")]
        public async Task<IActionResult> GetGroupUserParamsAsync([FromRoute] int groupId)
        {
            var result = await bsGroup.GetGroupUserParamsAsync(JwtStoredUserId, groupId);
            return Ok(result);
        }
    }
}
