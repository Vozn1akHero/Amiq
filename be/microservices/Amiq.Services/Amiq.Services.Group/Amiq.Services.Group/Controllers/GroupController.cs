using Amiq.Services.BusinessLayer;
using Amiq.Services.Group.Base;
using Amiq.Services.Group.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Amiq.Services.Group.Controllers
{
    public class GroupController : AmiqGroupBaseController
    {
        private BlGroup _blGroup = new BlGroup();

        [HttpPost]
        public async Task<IActionResult> CreateGroupAsync([FromBody] DtoCreateGroup dtoCreateGroup)
        {
            DtoGroupCard group = await _blGroup.CreateGroupAsync(JwtStoredUserId, dtoCreateGroup);
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
            var data = await _blGroup.GetByName(JwtStoredUserId, name);
            return Ok(data);
        }

        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetGroupById(int groupId, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return new StatusCodeResult(499);
            }
            var data = await _blGroup.GetGroupById(groupId);
            if(data == null) return NotFound();
            return Ok(data);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditBasicDataAsync([FromBody] DtoEditGroupDataRequest dtoEditGroupDataRequest)
        {
            var result = await _blGroup.EditAsync(dtoEditGroupDataRequest);
            return Ok(result);
        }

        [HttpGet("user-params/{groupId}")]
        public async Task<IActionResult> GetGroupUserParamsAsync([FromRoute] int groupId)
        {
            var result = await _blGroup.GetGroupUserParamsAsync(JwtStoredUserId, groupId);
            return Ok(result);
        }
    }
}
