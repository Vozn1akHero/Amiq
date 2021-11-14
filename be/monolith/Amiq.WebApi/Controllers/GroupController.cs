using Amiq.Business;
using Amiq.Common.DbOperation;
using Amiq.Contracts.Group;
using Amiq.WebApi.Base;
using Amiq.WebApi.Core.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    [Authorize]
    public class GroupController : AmiqBaseController
    {
        private BsGroup bsGroup = new BsGroup();

        [HttpPost]
        public async Task<IActionResult> CreateGroupAsync([FromBody] DtoCreateGroup dtoCreateGroup)
        {
            DtoGroupCard group = await bsGroup.CreateGroupAsync(JwtStoredUserInfo.UserId, dtoCreateGroup);
            return CreatedAtAction(nameof(CreateGroupAsync), group);
        }

        [HttpPost("drop")]
        [AuthorizeMainGroupAdmin]
        public async Task<IActionResult> DropGroup([FromBody] DtoDropGroupRequest dtoDropGroupRequest)
        {
            return await Task.FromResult(Ok());
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var data = await bsGroup.GetByName(JwtStoredUserInfo.UserId, name);
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
            var result = await bsGroup.GetGroupUserParamsAsync(JwtStoredUserInfo.UserId, groupId);
            return Ok(result);
        }
    }
}
